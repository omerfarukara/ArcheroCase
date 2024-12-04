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

        public int ArrowCountPerMultiplier => arrowCountPerMultiplier;
        public int ArrowBounceCountMultiplier => arrowBounceCountMultiplier;
        public int BurnDamageMultiplier => burnDamageMultiplier;
        public int BurnDurationMultiplier => burnDurationMultiplier;
        public int AttackSpeedMultiplier => attackSpeedMultiplier;
        

        [SerializeField] private AbilityType abilityType;


        public void Init(AbilityManager abilityManager)
        {

        }

        public void Active(AbilityManager abilityManager)
        {
            abilityManager.RageModeActive = true;
        }

        public void DeActive(AbilityManager abilityManager)
        {
            abilityManager.RageModeActive = false;
        }


        public AbilityType GetAbilityType()
        {
            return abilityType;
        }
    }
}