using System;
using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "AttackSpeedBoost", menuName = "Ability/AttackSpeedBoost")]
    public class AttackSpeed : ScriptableObject, IAbility
    {
        [SerializeField] private AbilityType abilityType;
        [SerializeField] private float defaultAttackSpeed;
        [SerializeField] private float attackSpeedMultiplier;

        public float AttackSpeedMultiplier => attackSpeedMultiplier;
        
        public void Init(AbilityManager abilityManager)
        {
            abilityManager.AttackSpeedTime = defaultAttackSpeed;
        }

        public void Active(AbilityManager abilityManager)
        {
            abilityManager.AttackSpeedTime = defaultAttackSpeed / attackSpeedMultiplier;
            abilityManager.ExtraAttackSpeedTimeActive = true;
        }

        public void DeActive(AbilityManager abilityManager)
        {
            abilityManager.AttackSpeedTime = defaultAttackSpeed;
            abilityManager.ExtraAttackSpeedTimeActive = false;
        }
        
        public AbilityType GetAbilityType()
        {
            return abilityType;
        }
    }
}