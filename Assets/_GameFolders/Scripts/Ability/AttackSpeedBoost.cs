using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "AttackSpeedBoost", menuName = "Ability/AttackSpeedBoost")]
    public class AttackSpeedBoost : ScriptableObject, IAbility
    {
        [SerializeField] private float attackSpeedMultiplier;
        [SerializeField] private AbilityType abilityType;
        
        public void Init(AbilityManager abilityManager)
        {
            abilityManager.AttackSpeedMultiplier = attackSpeedMultiplier;
        }

        public void Active(AbilityManager abilityManager)
        {
            abilityManager.ExtraAttackSpeedActive = true;
        }

        public void DeActive(AbilityManager abilityManager)
        {
            abilityManager.ExtraAttackSpeedActive = false;
        }
        
        public AbilityType GetAbilityType()
        {
            return abilityType;
        }
    }
}