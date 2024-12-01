using UnityEngine;

namespace _GameFolders.Scripts
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoSingleton<GameManager>
    {
        public CharacterController CharacterController { get; set; }
    }
}