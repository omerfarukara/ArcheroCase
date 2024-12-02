using UnityEngine;

namespace _GameFolders.Scripts
{
    public class AnimationController : MonoBehaviour
    {
        private Animation _animation;
        private UnityEngine.AnimationState _currentAnimationState;
        
        private float _animationTime;

        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        private void OnEnable()
        {
            GameEventManager.OnSetGameState += OnSetGameState;
        }

        private void OnDisable()
        {
            GameEventManager.OnSetGameState -= OnSetGameState;
        }

        private void OnSetGameState(GameState state)
        {
            if (state == GameState.Paused)
            {
                if (_currentAnimationState != null)
                {
                    _animationTime = _currentAnimationState.time;
                    _animation.Stop();
                }
            }
            else if (state == GameState.Playing)
            {
                if (_currentAnimationState != null)
                {
                    _currentAnimationState.time = _animationTime;
                    _animation.Play(_currentAnimationState.clip.name);
                }
            }
        }
        
        
        public void StopAnimation()
        {
            if (_currentAnimationState != null)
            {
                _animation.Stop();
                _currentAnimationState.time = 0;
                _animation.Sample();
            }
            _currentAnimationState = null;
        }
        

        public void PlayAnimation(PlayingAnimationState state)
        {
 
            _animation.Play(state.ToString());
            _currentAnimationState = _animation[state.ToString()];
        }

        public void ResumeAnimation()
        {
            if (_currentAnimationState != null)
            {
                _currentAnimationState.time = _animationTime;
                _animation.Play(_currentAnimationState.clip.name);
            }
        }

        public float GetAnimationLength(PlayingAnimationState state)
        {
            return _animation[state.ToString()].length;
        }

        public bool IsAnimationPlaying(PlayingAnimationState state)
        {
            return _animation.IsPlaying(state.ToString());
        }
    }
}