namespace _GameFolders.Scripts
{
    public enum PlayingAnimationState
    {
        Idle = 0,
        Run = 1,
        AttackEnd = 2,
        Attack = 3
    }

    public enum GameState
    {
        Playing = 0,
        Paused = 1
    }

    public enum AbilityType
    {
        ArrowBounce = 0,
        ArrowCountPerAttack = 1,
        AttackSpeedBoost = 2,
        BurnDamage = 3,
        RageMode = 4
    }

    public enum MoveState
    {
        NotMoving = 0,
        Moving = 1
    }
}