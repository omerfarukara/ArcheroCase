using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class CharacterController : BaseCharacter
    {
        private UIManager _uiManager;
        private GameManager _gameManager;

        private void Start()
        {
            _uiManager = UIManager.Instance;
            _gameManager = GameManager.Instance;
            _gameManager.CharacterController = this;
        }

        private void Update()
        {
            if (!IsAttack)
            {
                Character.Move(_uiManager.GetDirection());
            }

            SetAnimationState();
        }


        private void SetAnimationState()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PlayAnimation(AnimationState.Attack).Forget();
            }
            else
            {
                PlayAnimation(_uiManager.GetDirection().magnitude == 0 ? AnimationState.Idle : AnimationState.Run).Forget();
            }
        }
        
    }
}
