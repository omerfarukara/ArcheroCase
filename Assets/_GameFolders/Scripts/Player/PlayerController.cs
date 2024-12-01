using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class PlayerController : BaseCharacter
    {
        private UIManager _uiManager;

        private void Start()
        {
            _uiManager = UIManager.Instance;
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
