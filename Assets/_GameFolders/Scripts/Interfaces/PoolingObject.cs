using UnityEngine;

namespace _GameFolders.Scripts
{
    public abstract class PoolingObject : MonoBehaviour
    {
        public abstract void Initialize(Vector3 spawnPosition);

        public abstract void Close();
    }
}