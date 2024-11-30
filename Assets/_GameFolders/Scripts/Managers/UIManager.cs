using UnityEngine;

namespace _GameFolders.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [Header("[-- Joystick --]")]
        [SerializeField] private Joystick joystick;
        
        public Vector3 GetDirection()
        {
            return new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
    }
}
