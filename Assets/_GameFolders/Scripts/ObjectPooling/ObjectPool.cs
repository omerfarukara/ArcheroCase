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


        public T Get<T>() where T : PoolingObject
        {
            Type type = typeof(T);

            if (_pools.ContainsKey(type) && _pools[type].Count > 0)
            {
                PoolingObject obj = _pools[type].Dequeue();
                obj.gameObject.SetActive(true); 
                obj.Initialize(); 
                return (T)obj;
            }

            string prefabName = typeof(T).Name;
            
            if (_prefabDictionary.TryGetValue(prefabName, out GameObject prefab))
            {
                GameObject instance = Instantiate(prefab, transform);
                PoolingObject newObj = instance.GetComponent<PoolingObject>();
                newObj.Initialize();
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

            obj.gameObject.SetActive(false);
            _pools[type].Enqueue(obj);
        }
    }
}
