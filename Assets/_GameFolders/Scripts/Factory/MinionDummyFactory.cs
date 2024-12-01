using _GameFolders.Scripts.Factory;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class MinionDummyFactory : DummyFactory
    {
        [SerializeField] private MinionDummy minionDummyPrefab;
        
        public override BaseDummy CreateDummy(Vector3 position)
        {
            MinionDummy minionDummy = Instantiate(minionDummyPrefab,transform);
            minionDummy.Initialize(position);

            return minionDummy;
        }
    }
}