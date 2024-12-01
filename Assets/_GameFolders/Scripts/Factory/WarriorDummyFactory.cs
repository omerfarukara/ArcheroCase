using _GameFolders.Scripts.Factory;
using UnityEngine;
namespace _GameFolders.Scripts
{
    public class WarriorDummyFactory : DummyFactory
    {
        [SerializeField] private WarriorDummy warriorDummyPrefab;

        public override BaseDummy CreateDummy(Vector3 position)
        {
            WarriorDummy warriorDummy = Instantiate(warriorDummyPrefab, transform);
            warriorDummy.Initialize(position);

            return warriorDummy;
        }
    }
}