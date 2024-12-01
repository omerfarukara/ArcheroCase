using UnityEngine;

namespace _GameFolders.Scripts
{
    public class CharacterMovement
    {
        private readonly BaseCharacterData _baseCharacterData;
        private readonly Transform _transform;
        private readonly Rigidbody _rb;

        public CharacterMovement(BaseCharacterData baseCharacterData, Transform transform, Rigidbody rb)
        {
            _baseCharacterData = baseCharacterData;
            _transform = transform;
            _rb = rb;
        }

        public void Move(Vector3 direction)
        {
            if (_transform == null || direction.magnitude == 0)
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
                return;
            }

            float speed = _baseCharacterData.MoveSpeed;

            _transform.position += direction * (speed * Time.deltaTime);

            LookAtMovementDirection(direction);
        }

        private void LookAtMovementDirection(Vector3 direction)
        {
            if (direction.magnitude == 0) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _baseCharacterData.RotationSpeed * Time.deltaTime);
        }
    }
}