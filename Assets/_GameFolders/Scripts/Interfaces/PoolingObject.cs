using UnityEngine;

namespace _GameFolders.Scripts
{
    public abstract class PoolingObject : MonoBehaviour
    {
        public abstract void Initialize();

        public virtual void Close()
        {
            ObjectPool.Instance.Release(this);
        }
    }
}