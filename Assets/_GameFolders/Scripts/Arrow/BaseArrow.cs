using System.Collections.Generic;
using System.Threading;
using _GameFolders.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class BaseArrow : PoolingObject
    {
        [Header("[-- Projectile Motion Throw --]")]
        [SerializeField] private float gravity;
        [SerializeField] private float launchAngle;

        [Header("[-- Data --]")]
        [SerializeField] private int damage;
        [SerializeField] private float lifeTime;

        private GameManager _gameManager;
        private Rigidbody _rb;
        private Transform _target;
        private BaseDummy _lastHitDummy;
        private CancellationTokenSource _cancellationTokenSource;

        private bool _isMoving;
        private int _bouncedCount;
        private float _lifeTimer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _gameManager = GameManager.Instance;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable idDamageable))
            {
                _lastHitDummy = other.GetComponent<BaseDummy>();
                
                Burn();
                Bounce();

                idDamageable.TakeDamage(damage);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void Burn()
        {
            if (_gameManager.AbilityManager.BurnActive)
            {
                int burnDamage = (int)GameHelper.GetMultiplierAbilityCount(_gameManager.AbilityManager.BurnDamage, _gameManager.RageMode.BurnDamageMultiplier);

                float burnDuration = GameHelper.GetMultiplierAbilityCount(_gameManager.AbilityManager.BurnDuration, _gameManager.RageMode.BurnDurationMultiplier);

                _lastHitDummy.burnArrows.Add(new BurningArrowElement(burnDuration, burnDamage));
            }
        }

        private void Bounce()
        {
            if (_gameManager.AbilityManager.ArrowBounceActive)
            {
                int arrowBounceCount = (int)GameHelper.GetMultiplierAbilityCount(_gameManager.AbilityManager.ArrowBounceCount, _gameManager.RageMode.ArrowBounceCountMultiplier);

                if (_bouncedCount >= arrowBounceCount)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    _bouncedCount++;
                    List<BaseDummy> dummies = new List<BaseDummy>();
                    dummies.AddRange(EnemyManager.Instance.Dummies);
                    dummies.Remove(_lastHitDummy);
                    _target = GameHelper.FindSelectedSystemDummy(dummies,transform.position).transform;
                    Throw(_target).Forget();
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }


        private void Update()
        {
            if (!_gameManager.IsPlayable()) return;

            LifeTimer();
        }

        private void LifeTimer()
        {
            if (_lifeTimer < lifeTime)
            {
                _lifeTimer += Time.deltaTime;
                if (_lifeTimer > lifeTime)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public override void Initialize(Vector3 spawnPosition)
        {
            transform.position = spawnPosition;
            transform.rotation = Quaternion.identity;
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
            _bouncedCount = 0;
            _lastHitDummy = null;
        }


        public override void Close()
        {
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
            ObjectPool.Instance.Release(this);
        }

        private void OnDisable()
        {
            Close();
        }


        public async UniTask Throw(Transform target)
        {
            _cancellationTokenSource = new();
            this._target = target;

            await GameHelper.LaunchProjectileAsync(transform, _target, gravity, launchAngle, _gameManager, _cancellationTokenSource.Token);
        }


        private void OnApplicationQuit()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}