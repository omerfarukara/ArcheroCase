using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts
{
    public class Ability : MonoBehaviour
    {
        [Header("[-- Data --]")] [SerializeField]
        private ScriptableObject abilityScriptableObject;

        [Header("[-- Button --]")] [SerializeField]
        private Button abilityButton;

        [Header("[-- Initialize DoTween --]")] [SerializeField]
        private float initializeTweenValue;

        [SerializeField] private float initializeTweenDuration;
        [SerializeField] private float initializeDelay;
        [SerializeField] private Ease initializeEase;

        [Header("[-- UI --]")] [SerializeField]
        private List<Image> selectedFrames;

        [SerializeField] private Color selectedColor;
        [SerializeField] private Color notSelectedColor;

        private Tween _tween;
        private RectTransform _rectTransform;
        private IAbility _ability;
        private GameManager _gameManager;
        
        private Vector2 _defaultPos;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defaultPos = _rectTransform.anchoredPosition;
        }

        private void OnEnable()
        {
            InitializeAbility();
            GameEventManager.Activated += OnSelectedAbility;
        }

        private void Start()
        {
            AbilityInit();
            AddButtonListener();
            UpdateSelectedFrame();
        }

        private void AddButtonListener()
        {
            abilityButton.onClick.AddListener(ToggleAbility);
        }

        private void ToggleAbility()
        {
            if (_gameManager.AbilityManager.IsActiveAbility(_ability))
            {
                _gameManager.AbilityManager.DeActive(_ability);
                GameEventManager.DeActivated?.Invoke(_ability.GetAbilityType());
            }
            else
            {
                _gameManager.AbilityManager.Active(_ability);
                GameEventManager.Activated?.Invoke(_ability.GetAbilityType());
            }

            UpdateSelectedFrame();
        }

        private void InitializeAbility()
        {
            _ability = abilityScriptableObject as IAbility;
        }

        private void AbilityInit()
        {
            _gameManager = GameManager.Instance;
            _gameManager.AbilityManager?.Init(_ability);
        }

        private void OnSelectedAbility(AbilityType abilityType)
        {
            UpdateSelectedFrame();
        }

        private void UpdateSelectedFrame()
        {
            bool isActive = _gameManager.AbilityManager.IsActiveAbility(_ability);
            foreach (Image iImage in selectedFrames)
            {
                if (iImage != null)
                {
                    iImage.color = isActive ? selectedColor : notSelectedColor;
                }
            }
        }

        public void InitializeTween()
        {
            _tween = _rectTransform
                .DOAnchorPosY(initializeTweenValue, initializeTweenDuration)
                .SetDelay(initializeDelay)
                .SetEase(initializeEase).OnComplete(() => { abilityButton.interactable = true; });
        }

        public void CloseTween()
        {
            _tween?.Kill();
            
            abilityButton.interactable = false;
            _rectTransform.anchoredPosition = _defaultPos;
        }

        private void OnDisable()
        {
            _tween?.Kill();
            GameEventManager.Activated -= OnSelectedAbility;
        }
    }
}