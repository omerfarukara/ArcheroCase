using _GameFolders.Scripts.Factory;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class MinionDummyFactory : DummyFactory
    {
        public override BaseDummy CreateDummy(Vector3 position)
        {
            MinionDummy minionDummy = ObjectPool.Instance.Get<MinionDummy>(position);
            return minionDummy;
        }
    }
}