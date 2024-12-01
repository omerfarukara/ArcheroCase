using _GameFolders.Scripts.Factory;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class RogueDummyFactory : DummyFactory
    {
        [SerializeField] private RogueDummy rogueDummyPrefab;
        
        public override BaseDummy CreateDummy(Vector3 position)
        {
            RogueDummy rogueDummy = Instantiate(rogueDummyPrefab,transform);
            rogueDummy.Initialize(position);

            return rogueDummy;
        }
    }
}