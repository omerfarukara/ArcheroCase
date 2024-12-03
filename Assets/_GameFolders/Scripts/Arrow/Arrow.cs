using UnityEngine;

namespace _GameFolders.Scripts
{
    public class Arrow : PoolingObject
    {
        public override void Initialize()
        {
            Debug.Log("Initializing Arrow");
        }

        public override void Close()
        {
            base.Close();
            Debug.Log("Returning PoolingObject");
        }
    }
}