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

        [Header("[-- DoTween --]")] [SerializeField]
        private float onEnableTweenValue;

        [SerializeField] private float onEnableTweenDuration;
        [SerializeField] private float onEnableDelay;
        [SerializeField] private Ease onEnableEase;

        [Header("[-- UI --]")] [SerializeField]
        private List<Image> selectedFrames;

        [SerializeField] private Color selectedColor;
        [SerializeField] private Color notSelectedColor;

        private Tween _tween;
        private RectTransform _rectTransform;
        private IAbility _ability;
        private Vector2 _defaultPos;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defaultPos = _rectTransform.anchoredPosition;
        }

        private void OnEnable()
        {
            InitializeTween();
            InitializeAbility();
            GameEventManager.SelectedAbility += OnSelectedAbility;
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
            if (GameManager.Instance.AbilityManager.IsActiveAbility(_ability))
            {
                GameManager.Instance.AbilityManager.DeActive(_ability);
            }
            else
            {
                GameManager.Instance.AbilityManager.Active(_ability);
                GameEventManager.SelectedAbility?.Invoke(_ability.GetAbilityType());
            }

            UpdateSelectedFrame();
        }

        private void InitializeAbility()
        {
            _ability = abilityScriptableObject as IAbility;
        }

        private void AbilityInit()
        {
            GameManager.Instance.AbilityManager?.Init(_ability);
        }

        private void OnSelectedAbility(AbilityType abilityType)
        {
            if (abilityType == AbilityType.RageMode || _ability.GetAbilityType() == AbilityType.RageMode) return;

            if (abilityType != _ability.GetAbilityType())
            {
                GameManager.Instance.AbilityManager.DeActive(_ability);
                UpdateSelectedFrame();
            }
        }

        private void UpdateSelectedFrame()
        {
            bool isActive = GameManager.Instance.AbilityManager.IsActiveAbility(_ability);
            foreach (Image iImage in selectedFrames)
            {
                if (iImage != null)
                {
                    iImage.color = isActive ? selectedColor : notSelectedColor;
                }
            }
        }

        private void InitializeTween()
        {
            _tween = _rectTransform
                .DOAnchorPosY(onEnableTweenValue, onEnableTweenDuration)
                .SetDelay(onEnableDelay)
                .SetEase(onEnableEase);
        }

        private void OnDisable()
        {
            _tween?.Kill();
            _rectTransform.anchoredPosition = _defaultPos;
            GameEventManager.SelectedAbility -= OnSelectedAbility;
        }
    }
}