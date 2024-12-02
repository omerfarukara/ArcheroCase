using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "RageMode", menuName = "Ability/RageMode")]
    public class RageMode : ScriptableObject, IAbility
    {
        [SerializeField] private int arrowCountPerMultiplier;
        [SerializeField] private int arrowBounceCountMultiplier;
        [SerializeField] private int burnDamageMultiplier;
        [SerializeField] private int burnDurationMultiplier;
        [SerializeField] private int attackSpeedMultiplier;

        [SerializeField] private AbilityType abilityType;

        private int _defaultArrowCountPerMultiplier;
        private int _defaultArrowBounceCountMultiplier;
        private float _defaultBurnDamageMultiplier;
        private float _defaultBurnDurationMultiplier;
        private float _defaultAttackSpeedMultiplier;


        public void Init(AbilityManager abilityManager)
        {
            _defaultArrowCountPerMultiplier = abilityManager.ArrowCountPerAttack;
            _defaultArrowBounceCountMultiplier = abilityManager.ArrowBounceCount;
            _defaultBurnDamageMultiplier = abilityManager.BurnDamage;
            _defaultBurnDurationMultiplier = abilityManager.BurnDuration;
            _defaultAttackSpeedMultiplier = abilityManager.AttackSpeedMultiplier;
        }

        public void Active(AbilityManager abilityManager)
        {
            abilityManager.RageModeActive = true;

            abilityManager.ArrowCountPerAttack *= arrowCountPerMultiplier;
            abilityManager.ArrowBounceCount = arrowBounceCountMultiplier;
            abilityManager.BurnDamage *= burnDamageMultiplier;
            abilityManager.BurnDuration *= burnDurationMultiplier;
            abilityManager.AttackSpeedMultiplier *= attackSpeedMultiplier;
        }

        public void DeActive(AbilityManager abilityManager)
        {
            abilityManager.RageModeActive = false;

            abilityManager.ArrowCountPerAttack = _defaultArrowCountPerMultiplier;
            abilityManager.ArrowBounceCount = _defaultArrowBounceCountMultiplier;
            abilityManager.BurnDamage = _defaultBurnDamageMultiplier;
            abilityManager.BurnDuration = _defaultBurnDurationMultiplier;
            abilityManager.AttackSpeedMultiplier = _defaultAttackSpeedMultiplier;
        }
        
                
        public AbilityType GetAbilityType()
        {
            return abilityType;
        }
    }
}