using UnityEngine;

namespace _GameFolders.Scripts
{
    public class BaseCharacterData : ScriptableObject
    {
        [SerializeField] private int health;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        
        public int Health
        {
            get => health;
            set => health = value;
        }
        
        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }
        
        public float RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }
    }
}
