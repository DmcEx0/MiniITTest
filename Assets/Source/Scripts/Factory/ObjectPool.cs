using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.MergeTwo.Factory
{
    public class ObjectPool<T> where T : PoolableObject
    {
        private readonly T _prefab;
        private readonly int _poolSize;
        private readonly Transform _parent;

        private List<T> _pool;

        private readonly Func<T, T> _createdAction;

        public ObjectPool(T prefab, int poolSize, Transform parent, Func<T, T> createdAdditional)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _parent = parent;

            _createdAction = createdAdditional;
        }

        public void Initialize()
        {
            _pool = new List<T>();

            for (int i = 0; i < _poolSize; i++)
            {
                T instance = _createdAction(_prefab);

                instance.transform.SetParent(_parent);
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

            T newInstance = _createdAction(_prefab);
            newInstance.transform.SetParent(_parent);

            _pool.Add(newInstance);

            return newInstance;
        }
    }
}