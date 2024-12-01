using UnityEngine;

namespace _GameFolders.Scripts
{
    public class AnimationController : MonoBehaviour
    {
        private Animation _animation;

        private AnimationState _animationState;

        public AnimationState AnimationState
        {
            get => _animationState;
            set
            {
                _animationState = value;
                SetAnimation(value.ToString());
            }
        }

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        private void SetAnimation(string animationName)
        {
            _animation.Play(animationName);
        }
    }
}
