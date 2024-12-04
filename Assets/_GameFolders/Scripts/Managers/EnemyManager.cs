using System.Collections.Generic;
using _GameFolders.Scripts.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameFolders.Scripts
{
    public class EnemyManager : MonoSingleton<EnemyManager>
    {
        [Header("[-- Factory --]")]
        [SerializeField] private DummyFactory[] factories;
        
        [Header("[-- Settings --]")] 
        [SerializeField] private int initialDummyCount = 5;
        [SerializeField] private Vector2 spawnAreaMin;
        [SerializeField] private Vector2 spawnAreaMax;
        [Range(0,2)][SerializeField] private float minSpawnDistance = 2f;

        private List<BaseDummy> _dummies = new();
        public List<BaseDummy> Dummies => _dummies;

        private GameManager _gameManager;
        

        private void OnEnable()
        {
            GameEventManager.OnKillEnemy += OnDummyKilled;
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;

            InitializeDummies();
        }

        private void OnDisable()
        {
            GameEventManager.OnKillEnemy -= OnDummyKilled;
        }

        private void InitializeDummies()
        {
            for (int i = 0; i < initialDummyCount; i++)
            {
                SpawnDummy();
            }
        }

        private void SpawnDummy()
        {
            Vector3 spawnPosition;

            do
            {
                spawnPosition = GetRandomPosition();
            } while (!IsPositionValid(spawnPosition));

            DummyFactory dummyFactory = factories[Random.Range(0, factories.Length)];
            BaseDummy dummy = dummyFactory.CreateDummy(spawnPosition);

            _dummies.Add(dummy);
        }

        private Vector3 GetRandomPosition()
        {
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float z = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            return new Vector3(x, 0, z);
        }

        private bool IsPositionValid(Vector3 position)
        {
            if (Vector3.Distance(_gameManager.CharacterController.transform.position, position) < minSpawnDistance)
            {
                return false;
            }
            
            foreach (BaseDummy dummy in _dummies)
            {
                if (Vector3.Distance(dummy.transform.position, position) < minSpawnDistance)
                {
                    return false;
                }
            }
            
            return true;
        }

        private void OnDummyKilled(BaseDummy baseDummy)
        {
            _dummies.Remove(baseDummy);
            
            SpawnDummy();
        }
    }
}
