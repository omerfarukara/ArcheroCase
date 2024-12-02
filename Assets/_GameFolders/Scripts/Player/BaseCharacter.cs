using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class BaseCharacter : MonoBehaviour
    {
        [Header("[-- Data --]")] [SerializeField]
        private PlayerData playerData;

        [Header("[-- Animation --]")] [SerializeField]
        protected AnimationController animationController;

        private Rigidbody _rb;

        private bool _isAttack;
        protected bool IsAttack => _isAttack;

        protected CharacterMovement Character;

        protected MoveState MoveState;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Character = new CharacterMovement(playerData, transform, _rb);
        }



        protected async UniTask PlayAnimation(PlayingAnimationState playingAnimationState)
        {
            if (_isAttack) return;

            if (playingAnimationState == PlayingAnimationState.Attack)
            {
                _isAttack = true;
            }

            await PlaySingleAnimation(playingAnimationState);

            if (playingAnimationState == PlayingAnimationState.Attack)
            {
                await PlaySingleAnimation(PlayingAnimationState.AttackEnd);
                _isAttack = false;
            }
        }

        private async UniTask PlaySingleAnimation(PlayingAnimationState playingAnimationState)
        {
            
            animationController.PlayAnimation(playingAnimationState);

            float animationLength = animationController.GetAnimationLength(playingAnimationState);
            float elapsedTime = 0;

            while (elapsedTime < animationLength)
            {
                if (GameManager.Instance.GameState == GameState.Paused)
                {
                    await UniTask.WaitUntil(() => GameManager.Instance.GameState == GameState.Playing);
                    animationController.ResumeAnimation();
                }

                elapsedTime += Time.deltaTime;

                if (!animationController.IsAnimationPlaying(playingAnimationState))
                {
                    break;
                }

                await UniTask.Yield();
            }
        }
    }
}
