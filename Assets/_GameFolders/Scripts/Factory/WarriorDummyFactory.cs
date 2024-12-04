using _GameFolders.Scripts.Factory;
using UnityEngine;
namespace _GameFolders.Scripts
{
    public class WarriorDummyFactory : DummyFactory
    {
        public override BaseDummy CreateDummy(Vector3 position)
        {
            WarriorDummy warriorDummy = ObjectPool.Instance.Get<WarriorDummy>(position);
            return warriorDummy;
        }
    }
}