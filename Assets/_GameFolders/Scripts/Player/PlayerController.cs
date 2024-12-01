using UnityEngine;

namespace _GameFolders.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("[-- Data --]")]
        [SerializeField] private BaseCharacterData playerData;


        [Header("[-- Animation --]")]
        [SerializeField] private AnimationController animationController;

        private BaseCharacter _character;
        private UIManager _uiManager;
        

        private void Awake()
        {
            _character = new BaseCharacter(playerData, transform,animationController);
        }

        private void Start()
        {
            _uiManager = UIManager.Instance;
        }

        private void Update()
        {
            _character.Move(_uiManager.GetDirection());

            SetAnimationState();
        }

        private void SetAnimationState()
        {
            animationController.AnimationState = _uiManager.GetDirection().magnitude == 0 ? AnimationState.Idle : AnimationState.Run;
        }
    }
}