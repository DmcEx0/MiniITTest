using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.Factory
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly int _poolSize;
        private readonly Transform _parent;

        private readonly List<T> _pool;

        public ObjectPool(T prefab, int poolSize, Transform parent)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _parent = parent;
            _pool = InitializePool();
        }

        public ObjectPool(Transform parent)
        {
            _parent = parent;
            _pool = new List<T>();
        }

        public T GetAvailable()
        {
            foreach (var instance in _pool)
            {
                if (instance.gameObject.activeSelf == false)
                {
                    return instance;
                }
            }

            var newInstance = GameObject.Instantiate(_prefab, _parent);
            _pool.Add(newInstance);
            return newInstance;
        }

        private List<T> InitializePool()
        {
            var pool = new List<T>();

            for (int i = 0; i < _poolSize; i++)
            {
                var instance = GameObject.Instantiate(_prefab, _parent);
                pool.Add(instance);
                instance.gameObject.SetActive(false);
            }

            return pool;
        }
    }
}