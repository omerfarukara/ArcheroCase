using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "DoubleArrow", menuName = "Ability/DoubleArrow")]
    public class ArrowCountPerAttack : ScriptableObject, IAbility
    {
        [SerializeField] private int arrowCountPerAttack;
        [SerializeField] private AbilityType abilityType;

        public void Init(AbilityManager abilityManager)
        {
            abilityManager.ArrowCountPerAttack = arrowCountPerAttack;
        }

        public void Active(AbilityManager abilityManager)
        {
            abilityManager.ExtraArrowActive = true;
        }

        public void DeActive(AbilityManager abilityManager)
        {
            abilityManager.ExtraArrowActive = false;
        }

        public AbilityType GetAbilityType()
        {
            return abilityType;
        }
    }
}