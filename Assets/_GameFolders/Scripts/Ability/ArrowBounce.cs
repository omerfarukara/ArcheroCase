using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "ArrowBounce", menuName = "Ability/ArrowBounce")]
    public class ArrowBounce : ScriptableObject, IAbility
    {
        [SerializeField] private int arrowBounceCount;
        [SerializeField] private AbilityType abilityType;
        
        public void Init(AbilityManager abilityManager)
        {
            abilityManager.ArrowBounceCount = arrowBounceCount;
        }

        public void Active(AbilityManager abilityManager)
        {
            abilityManager.ArrowBounceActive = true;
        }

        public void DeActive(AbilityManager abilityManager)
        {
            abilityManager.ArrowBounceActive = false;
        }

        public AbilityType GetAbilityType()
        {
            return abilityType;
        }
    }
}