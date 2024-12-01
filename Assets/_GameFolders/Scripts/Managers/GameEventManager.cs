using System;

namespace _GameFolders.Scripts
{
    public static class GameEventManager
    {
        public static Action<BaseDummy> OnKillEnemy { get; set; }
    }
}