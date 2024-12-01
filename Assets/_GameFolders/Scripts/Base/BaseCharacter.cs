using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class BaseCharacter : MonoBehaviour
    {
        [Header("[-- Data --]")] [SerializeField]
        private BaseCharacterData playerData;

        [Header("[-- Animation --]")] [SerializeField]
        private AnimationController animationController;


        private Rigidbody _rb;


        private bool _isAttack;
        protected bool IsAttack => _isAttack;


        protected CharacterMovement Character;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Character = new CharacterMovement(playerData, transform, _rb);
        }

        protected async UniTask PlayAnimation(AnimationState animationState)
        {
            if (_isAttack) return;

            if (animationState == AnimationState.Attack)
            {
                _isAttack = true;
            }

            await PlaySingleAnimation(animationState);

            if (animationState == AnimationState.Attack)
            {
                await PlaySingleAnimation(AnimationState.AttackEnd);
                _isAttack = false;
            }
        }

        private async UniTask PlaySingleAnimation(AnimationState animationState)
        {
            animationController.PlayAnimation(animationState);

            float animationLength = animationController.GetAnimationLength(animationState);
            await UniTask.Delay((int)(animationLength * 1000));
        }
    }
}