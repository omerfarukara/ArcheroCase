namespace _GameFolders.Scripts
{
    public interface IAbility
    {
        public void Init(AbilityManager abilityManager);
        public void Active(AbilityManager abilityManager);
        public void DeActive(AbilityManager abilityManager);
        public AbilityType GetAbilityType();
    }
}