using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class CharacterController : BaseCharacter
    {
        [Header("[-- Arrow --]")]
        [SerializeField] private Transform spawnTransform;

        private BaseDummy _nearestDummy;
        private UIManager _uiManager;
        private GameManager _gameManager;
        private EnemyManager _enemyManager;
        private CancellationTokenSource _cancellationTokenSource;

        private bool _hasAttackedOnce;
        private float _currentAttackTimer;

        private void Start()
        {
            _enemyManager = EnemyManager.Instance;
            _uiManager = UIManager.Instance;
            _gameManager = GameManager.Instance;
            _gameManager.CharacterController = this;
        }

        private void Update()
        {
            if (!_gameManager.IsPlayable()) return;

            _nearestDummy = GameHelper.FindNearestDummy(_enemyManager.Dummies,transform.position);

            MoveOrAttack();
        }

        private void MoveOrAttack()
        {
            Character.Move(_uiManager.GetDirection());
            MoveState = _uiManager.IsMove() ? MoveState.NotMoving : MoveState.Moving;

            switch (MoveState)
            {
                case MoveState.NotMoving:
                {
                    PlayAnimation(PlayingAnimationState.Idle).Forget();

                    if (!_hasAttackedOnce)
                    {
                        Attack().Forget();
                        _hasAttackedOnce = true;
                    }

                    float attackSpeed = GameHelper.GetDivisionCount
                        (_gameManager.AbilityManager.AttackSpeedTime, _gameManager.RageMode.AttackSpeedMultiplier);

                    if (_currentAttackTimer < attackSpeed)
                    {
                        _currentAttackTimer += Time.deltaTime;

                        if (_currentAttackTimer >= attackSpeed)
                        {
                            Attack().Forget();
                        }
                    }
                    else
                    {
                        _currentAttackTimer = 0;
                    }

                    break;
                }
                case MoveState.Moving:
                {
                    _hasAttackedOnce = false;
                    _currentAttackTimer = 0;

                    if (!animationController.IsAnimationPlaying(PlayingAnimationState.Run))
                    {
                        animationController.StopAnimation();
                    }

                    PlayAnimation(PlayingAnimationState.Run).Forget();

                    break;
                }
            }
        }


        private async UniTask Attack()
        {
            _cancellationTokenSource = new();

            if (_nearestDummy != null)
            {
                transform.LookAt(_nearestDummy.transform);

                await PlayAnimation(PlayingAnimationState.Attack, async () =>
                {
                    if (!_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        if (_gameManager.AbilityManager.ExtraArrowActive)
                        {
                            int arrowCountPerAttack = (int)GameHelper.GetMultiplierAbilityCount
                                (_gameManager.AbilityManager.ArrowCountPerAttack, _gameManager.RageMode.ArrowCountPerMultiplier);

                            for (int i = 0; i < arrowCountPerAttack; i++)
                            {
                                while (_gameManager.GameState == GameState.Paused)
                                {
                                    await UniTask.Yield(_cancellationTokenSource.Token);
                                }

                                
                                BaseArrow baseArrow = ObjectPool.Instance.Get<BaseArrow>(spawnTransform.position);
                                if (baseArrow != null)
                                {
                                    baseArrow.Throw(_nearestDummy.transform).Forget();
                                }
                        
                                try
                                {
                                    await UniTask.Delay(TimeSpan.FromSeconds(
                                        0.1f
                                    ), cancellationToken: _cancellationTokenSource.Token);
                                }
                                catch (OperationCanceledException)
                                {
                                    Debug.Log("Delay iptal");
                                }
                            }
                        }
                        else
                        {
                            while (_gameManager.GameState == GameState.Paused)
                            {
                                await UniTask.Yield(_cancellationTokenSource.Token);
                            }

                            BaseArrow baseArrow = ObjectPool.Instance.Get<BaseArrow>(spawnTransform.position);
                            baseArrow.Throw(_nearestDummy.transform).Forget();
                        }
                    }
                });
            }
        }

        private void OnApplicationQuit()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}