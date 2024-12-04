using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GameFolders.Scripts
{
    [Serializable]
    public class PrefabTypePair
    {
        public GameObject prefab;
    }

    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        [SerializeField] private List<PrefabTypePair> prefabMappings;

        private readonly Dictionary<Type, Queue<PoolingObject>> _pools = new();
        private readonly Dictionary<string, GameObject> _prefabDictionary = new();

        protected override void Awake()
        {
            base.Awake();
            
            foreach (PrefabTypePair mapping in prefabMappings)
            {
                if (mapping.prefab != null)
                {
                    _prefabDictionary[mapping.prefab.name] = mapping.prefab;
                }
            }
        }
        
        public T Get<T>(Vector3 spawnPoint) where T : PoolingObject
        {
            Type targetType = typeof(T);

            if (_pools.ContainsKey(targetType) && _pools[targetType].Count > 0)
            {
                PoolingObject obj = _pools[targetType].Dequeue();
                if(obj == null) return null;
                obj.gameObject.SetActive(true);
                obj.Initialize(spawnPoint);
                return (T)obj;
            }

            string prefabName = targetType.Name;

            if (_prefabDictionary.TryGetValue(prefabName, out GameObject prefab))
            {
                GameObject instance = Instantiate(prefab);
                PoolingObject newObj = instance.GetComponent<PoolingObject>();
                newObj.Initialize(spawnPoint);
                return (T)newObj;
            }

            return null;
        }

        public void Release<T>(T obj) where T : PoolingObject
        {
            Type type = typeof(T);

            if (!_pools.ContainsKey(type))
            {
                _pools[type] = new Queue<PoolingObject>();
            }

            _pools[type].Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
    }
}