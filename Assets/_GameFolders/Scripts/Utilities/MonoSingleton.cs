using UnityEngine;

namespace _GameFolders.Scripts
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => _instance;

        [SerializeField] private bool dontDestroyOnLoad; 
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                if (dontDestroyOnLoad)
                {
                    transform.parent = null;
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}