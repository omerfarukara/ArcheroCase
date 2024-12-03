using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts
{
    [DefaultExecutionOrder(-10)]
    public class UIManager : MonoSingleton<UIManager>
    {
        [Header("[-- Joystick --]")] [SerializeField]
        private Joystick joystick;

        [Header("[-- Ability --]")]
        [SerializeField] private GameObject abilityPanel;
        [SerializeField] private Button abilityButton;

        protected override void Awake()
        {
            base.Awake();
            abilityButton.onClick.AddListener(AbilityProcess);
        }

        private void AbilityProcess()
        {
            abilityPanel.SetActive(!abilityPanel.activeInHierarchy);
            
            GameEventManager.OnSetGameState?.Invoke(GameManager.Instance.GameState == GameState.Playing ? GameState.Paused : GameState.Playing);
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