using System;
using System.Collections.Generic;
using _GameFolders.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts
{
    public class BaseDummy : PoolingObject, IDamageable
    {
        [Header("[-- Data --]")]
        [SerializeField] private EnemyData enemyData;

        [Header("[-- Hp --]")]
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Image fillImage;

        [Header("[-- Hp Colors --]")]
        [SerializeField] private Color green;
        [SerializeField] private Color yellow;
        [SerializeField] private Color orange;
        [SerializeField] private Color red;

        private float _healthPercentage;
        private float _health;
        private float _burnTimer;

        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                _healthPercentage = Health / enemyData.Health;
            }
        }

        public List<BurningArrowElement> burnArrows = new();

        public override void Initialize(Vector3 spawnPosition)
        {
            transform.position = spawnPosition;
            HealthInit();
        }

        private void Update()
        {
            if (!GameManager.Instance.IsPlayable()) return;

            for (int i = burnArrows.Count - 1; i >= 0; i--)
            {
                BurningArrowElement burningArrow = burnArrows[i];
                if (burningArrow.takenDamageTimeCount < burningArrow.burnDuration)
                {
                    if (burningArrow.burnTimer <= 1)
                    {
                        burningArrow.burnTimer += Time.deltaTime;
                        if (burningArrow.burnTimer > 1)
                        {
                            burningArrow.takenDamageTimeCount++;
                            TakeDamage(burningArrow.burnDamage);
                            burningArrow.burnTimer = 0;
                        }
                    }
                }
                else
                {
                    burnArrows.RemoveAt(i);
                }
            }
        }


        public void TakeDamage(int damageValue)
        {
            if (Health <= 0) return;

            Health -= damageValue;

            if (Health <= 0)
            {
                GameEventManager.OnKillEnemy?.Invoke(this);
            }

            hpSlider.DOValue(_healthPercentage, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                if (Health <= 0)
                {
                    gameObject.SetActive(false);
                }
            });

            UpdateHealthColor();
        }

        private void HealthInit()
        {
            Health = enemyData.Health;
            UpdateHealthColor();
            hpSlider.DOValue(_healthPercentage, 0.05f);
        }

        private void UpdateHealthColor()
        {
            Color targetColor = _healthPercentage switch
            {
                > 0.75f => green,
                > 0.5f => yellow,
                > 0.25f => orange,
                _ => red
            };
            fillImage.DOColor(targetColor, 0.5f).SetEase(Ease.OutCubic);
        }

        public override void Close()
        {
            burnArrows.Clear();
        }

        private void OnDisable()
        {
            Close();
        }
    }
    
    [Serializable]
    public class BurningArrowElement
    {
        public float burnDuration;
        public int burnDamage;
        public float burnTimer;
        public int takenDamageTimeCount;

        public BurningArrowElement(float burnDuration, int burnDamage)
        {
            this.burnDuration = burnDuration;
            this.burnDamage = burnDamage;
        }
    }
}