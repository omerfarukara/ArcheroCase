using UnityEngine;

namespace _GameFolders.Scripts
{
    public class CharacterMovement
    {
        private readonly CharacterData _characterData;
        private readonly Transform _transform;
        private readonly Rigidbody _rb;


        public CharacterMovement(CharacterData baseCharacterData, Transform transform, Rigidbody rb)
        {
            _characterData = baseCharacterData;
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

            float speed = _characterData.MoveSpeed;

            _transform.position += direction * (speed * Time.deltaTime);
            LookAtMovementDirection(direction);
        }

        private void LookAtMovementDirection(Vector3 direction)
        {
            if (direction.magnitude == 0) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _characterData.RotationSpeed * Time.deltaTime);
        }
    }
}