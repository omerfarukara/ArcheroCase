﻿using System;

namespace _GameFolders.Scripts
{
    public static class GameEventManager
    {
        public static Action<BaseDummy> OnKillEnemy { get; set; }
        public static Action<GameState> OnSetGameState { get; set; }
        public static Action<AbilityType> Activated { get; set; }
        public static Action<AbilityType> DeActivated { get; set; }
    }
}