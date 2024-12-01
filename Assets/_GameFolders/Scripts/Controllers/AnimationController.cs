using UnityEngine;

namespace _GameFolders.Scripts
{
    public class AnimationController : MonoBehaviour
    {
        private Animation _animation;

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        public void PlayAnimation(AnimationState state)
        {
            _animation.Play(state.ToString());
        }

        public float GetAnimationLength(AnimationState state)
        {
            return _animation[state.ToString()].length;
        }
    }
}