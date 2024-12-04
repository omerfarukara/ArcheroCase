using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class BaseCharacter : MonoBehaviour
    {
        [Header("[-- Data --]")]
        [SerializeField] private CharacterData characterData;

        [Header("[-- Animation --]")]
        [SerializeField] protected AnimationController animationController;

        private Rigidbody _rb;
        protected CharacterMovement Character;
        
        protected MoveState MoveState;
        
        private bool _isAttack;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Character = new CharacterMovement(characterData, transform, _rb);
        }

        protected async UniTask PlayAnimation(PlayingAnimationState playingAnimationState, Action attackEnd = null)
        {
            if (_isAttack) return;

            if (playingAnimationState == PlayingAnimationState.Attack)
            {
                _isAttack = true;
            }

            await PlaySingleAnimation(playingAnimationState);

            if (playingAnimationState == PlayingAnimationState.Attack)
            {
                if (MoveState == MoveState.NotMoving)
                {
                    attackEnd?.Invoke();
                }
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
