using _GameFolders.Scripts.Factory;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class RogueDummyFactory : DummyFactory
    {
        public override BaseDummy CreateDummy(Vector3 position)
        {
            RogueDummy rogueDummy = ObjectPool.Instance.Get<RogueDummy>(position);
            return rogueDummy;
        }
    }
}