using UnityEngine;

namespace _GameFolders.Scripts
{
    public class AnimationController : MonoBehaviour
    {
        private Animation _animation;
        private AnimationState _currentAnimationState;
        private GameManager _gameManager;

        private float _animationTime;

        private float _attackAnimationSpeed;
        private float _attackEndAnimationSpeed;

        private void Awake()
        {
            _animation = GetComponent<Animation>();

            _attackAnimationSpeed = _animation[GameConstants.Animations.ATTACK].speed * 2;
            _attackEndAnimationSpeed = _animation[GameConstants.Animations.ATTACK].speed * 2;
            AttackSpeedIncrease(1);
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        private void AttackSpeedIncrease(float speedMultiplier)
        {
            _animation[GameConstants.Animations.ATTACK].speed = _attackAnimationSpeed * speedMultiplier;
            _animation[GameConstants.Animations.ATTACK_END].speed = _attackEndAnimationSpeed * speedMultiplier;
        }

        private void AttackSpeedDefault()
        {
            _animation[GameConstants.Animations.ATTACK].speed = _attackAnimationSpeed;
            _animation[GameConstants.Animations.ATTACK_END].speed = _attackEndAnimationSpeed;
        }

        private void OnEnable()
        {
            GameEventManager.OnSetGameState += OnSetGameState;
            GameEventManager.Activated += OnCheckAbility;
            GameEventManager.DeActivated += OnCheckAbility;
        }

        private void OnDisable()
        {
            GameEventManager.OnSetGameState -= OnSetGameState;
            GameEventManager.Activated -= OnCheckAbility;
            GameEventManager.DeActivated -= OnCheckAbility;
        }

        private void OnCheckAbility(AbilityType abilityType)
        {
            if (_gameManager.AbilityManager.ExtraAttackSpeedTimeActive)
            {
                float multiplier = _gameManager.AttackSpeed.AttackSpeedMultiplier;

                if (_gameManager.AbilityManager.RageModeActive)
                {
                    multiplier *= _gameManager.RageMode.AttackSpeedMultiplier;
                }
                
                AttackSpeedIncrease(multiplier);
            }
            else
            {
                AttackSpeedDefault();
            }
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