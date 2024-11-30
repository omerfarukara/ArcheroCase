using UnityEngine;

namespace _GameFolders.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private BaseCharacterData playerData;

        private BaseCharacter _character;

        private void Awake()
        {
            _character = new BaseCharacter(playerData, transform);
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical);

            _character.Move(direction);
        }
    }
}