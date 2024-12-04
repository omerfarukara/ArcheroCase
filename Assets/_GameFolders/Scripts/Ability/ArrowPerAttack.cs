using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "DoubleArrow", menuName = "Ability/DoubleArrow")]
    public class ArrowPerAttack : ScriptableObject, IAbility
    {
        [SerializeField] private AbilityType abilityType;
     
        [SerializeField] private int arrowCountPerAttack;

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