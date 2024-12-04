using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts
{
    [DefaultExecutionOrder(-10)]
    public class UIManager : MonoSingleton<UIManager>
    {
        [Header("[-- Joystick --]")]
        [SerializeField] private Joystick joystick;

        [Header("[-- Ability --]")]
        [SerializeField] private GameObject abilityBackgroundPanel;
        [SerializeField] private Button abilityButton;
        [SerializeField] private List<Ability> abilities;
        
        protected override void Awake()
        {
            base.Awake();
            abilityButton.onClick.AddListener(AbilityProcess);
        }

        private void AbilityProcess()
        {
            if (GameManager.Instance.GameState == GameState.Paused)
            {
                abilityBackgroundPanel.SetActive(false);
                foreach (Ability ability in abilities)
                {
                    ability.CloseTween();
                }
                GameEventManager.OnSetGameState?.Invoke(GameState.Playing);
            }
            else
            {
                abilityBackgroundPanel.SetActive(true);
                foreach (Ability ability in abilities)
                {
                    ability.InitializeTween();
                }
                GameEventManager.OnSetGameState?.Invoke(GameState.Paused);
            }
        }

        public Vector3 GetDirection()
        {
            return new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }

        public bool IsMove()
        {
            return GetDirection().magnitude == 0;
        }
    }
}