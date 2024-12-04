using _GameFolders.Scripts.Factory;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class MageDummyFactory : DummyFactory
    {
        public override BaseDummy CreateDummy(Vector3 position)
        {
            MageDummy mageDummy = ObjectPool.Instance.Get<MageDummy>(position);
            return mageDummy;
        }

    }
}