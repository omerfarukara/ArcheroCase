namespace _GameFolders.Scripts
{
    public class AbilityManager
    {
        public int ArrowCountPerAttack { get; set; }
        public int ArrowBounceCount { get; set; }
        public float BurnDamage { get; set; }
        public float BurnDuration { get; set; }
        public float AttackSpeedTime { get; set; }
        
        
        public bool ExtraArrowActive { get; set; }
        public bool ArrowBounceActive { get; set; }
        public bool BurnActive { get; set; }
        public bool ExtraAttackSpeedTimeActive { get; set; }
        public bool RageModeActive { get; set; }
        
        public bool IsActiveAbility(IAbility ability)
        {
            switch (ability)
            {
                case ArrowBounce:
                    return ArrowBounceActive;
                case ArrowPerAttack:
                    return ExtraArrowActive;
                case AttackSpeed:
                    return ExtraAttackSpeedTimeActive;
                case BurningArrow:
                    return BurnActive;
                case RageMode:
                    return RageModeActive;
                default:
                    return false;
            }
        }

        public void Init(IAbility ability)
        {
            ability.Init(this);
        }

        public void Active(IAbility ability)
        {
            ability.Active(this);
        }
        
        public void DeActive(IAbility ability)
        {
            ability.DeActive(this);
        }
    }
}