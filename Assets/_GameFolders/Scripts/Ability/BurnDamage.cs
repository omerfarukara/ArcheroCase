using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "BurnDamage", menuName = "Ability/BurnDamage")]
    public class BurnDamage : ScriptableObject, IAbility
    {
        [SerializeField] private int burnDamage;
        [SerializeField] private float burnDuration;
        [SerializeField] private AbilityType abilityType;
        
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