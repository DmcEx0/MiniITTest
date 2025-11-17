using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.Factory
{
    public class ObjectPool<T> where T : PoolableObject
    {
        private readonly T _prefab;
        private readonly int _poolSize;
        private readonly Transform _parent;

        private List<T> _pool;

        public ObjectPool(T prefab, int poolSize, Transform parent)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _parent = parent;
        }
        
        public void Initialize()
        {
            _pool = new List<T>();

            for (int i = 0; i < _poolSize; i++)
            {
                T instance = Object.Instantiate(_prefab, _parent);
                _pool.Add(instance);
                instance.gameObject.SetActive(false);
            }
        }

        public T Get()
        {
            var instance = GetAvailable();

            instance.Disabled += OnDisable;

            return instance;
        }

        private void OnDisable(PoolableObject obj)
        {
            obj.Disabled -= OnDisable;
            
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parent);
        }

        private T GetAvailable()
        {
            foreach (var instance in _pool)
            {
                if (instance.gameObject.activeSelf == false)
                {
                    instance.gameObject.SetActive(true);
                    
                    return instance;
                }
            }

            T newInstance = Object.Instantiate(_prefab, _parent);
            _pool.Add(newInstance);
            
            return newInstance;
        }
    }
}