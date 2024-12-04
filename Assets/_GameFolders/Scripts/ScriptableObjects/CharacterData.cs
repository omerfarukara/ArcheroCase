using UnityEngine;

namespace _GameFolders.Scripts
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Data / Player Data")]
    public class CharacterData : BaseCharacterData
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        
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