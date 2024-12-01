using UnityEngine;

namespace _GameFolders.Scripts.Factory
{
    public abstract class DummyFactory : MonoBehaviour
    {
        public abstract BaseDummy CreateDummy(Vector3 position);

    }
}