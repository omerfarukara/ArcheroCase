using _GameFolders.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts
{
    public class BaseDummy : MonoBehaviour, IDamageable
    {
        [Header("[-- Data --]")] [SerializeField]
        private EnemyData enemyData;

        [Header("[-- Hp --]")] [SerializeField]
        private Slider hpSlider;

        [SerializeField] private Image fillImage;

        [Header("[-- Hp Colors --]")] [SerializeField]
        private Color green;

        [SerializeField] private Color yellow;
        [SerializeField] private Color orange;
        [SerializeField] private Color red;

        private float _healthPercentage;
        

        private float _health;

        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                _healthPercentage = Health / enemyData.Health;
            }
        }

        
        public void Initialize(Vector3 position)
        {
            transform.position = position;
            
            HealthInit();
            UpdateHealthColor();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                TakeDamage(25);
            }
        }


        public void TakeDamage(int damageValue)
        {
            if (Health <= 0) return;

            Health -= damageValue;
            
            hpSlider.DOValue(_healthPercentage, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                if (Health <= 0)
                {
                    Debug.Log("Down!", gameObject);
                    GameEventManager.OnKillEnemy?.Invoke(this);
                }
            });

            UpdateHealthColor();
        }

        private void HealthInit()
        {
            Health = enemyData.Health;
        }

        private void UpdateHealthColor()
        {
            Color targetColor = _healthPercentage switch
            {
                > 0.75f => green,
                > 0.5f => yellow,
                > 0.25f => orange,
                > 0 => red,
                _ => Color.black
            };
            fillImage.DOColor(targetColor, 0.5f).SetEase(Ease.OutCubic);
        }
    }
}