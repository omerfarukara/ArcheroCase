using UnityEngine;

namespace _GameFolders.Scripts
{
    [DefaultExecutionOrder(-10)]
    public class UIManager : MonoSingleton<UIManager>
    {
        [Header("[-- Joystick --]")]
        [SerializeField] private Joystick joystick;
        
        public Vector3 GetDirection()
        {
            return new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
    }
}
