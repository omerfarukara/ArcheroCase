using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "BurnDamage", menuName = "Ability/BurnDamage")]
    public class BurningArrow : ScriptableObject, IAbility
    {
        [SerializeField] private AbilityType abilityType;
        
        [SerializeField] private float burnDamage;
        [SerializeField] private float burnDuration;
        
        public void Init(AbilityManager abilityManager)
        {
            abilityManager.BurnDamage = burnDamage;
            abilityManager.BurnDuration = burnDuration;
        }

        public void Active(AbilityManager abilityManager)
        {
            abilityManager.BurnActive = true;
        }

        public void DeActive(AbilityManager abilityManager)
        {
            abilityManager.BurnActive = false;
        }
        
        public AbilityType GetAbilityType()
        {
            return abilityType;
        }
    }
}