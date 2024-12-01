using UnityEngine;

namespace _GameFolders.Scripts
{
    public class BaseCharacterData : ScriptableObject
    {
        [SerializeField] private float health;

        public float Health
        {
            get => health;
            set => health = value;
        }
    }
}
