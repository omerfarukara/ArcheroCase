using UnityEngine;

namespace _GameFolders.Scripts
{
    public class BaseCharacter
    {
        private readonly BaseCharacterData _baseCharacterData;
        private readonly Transform _transform;
        private readonly AnimationController _animationController;

        public BaseCharacter(BaseCharacterData baseCharacterData, Transform transform,AnimationController animationController)
        {
            _baseCharacterData = baseCharacterData;
            _transform = transform;
            _animationController = animationController;
        }

        public void Move(Vector3 direction)
        {
            if (_transform == null || direction.magnitude == 0) return;
            
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