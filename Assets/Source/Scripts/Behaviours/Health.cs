using System;
using MiniIT.Factory;
using UnityEngine;

namespace MiniIT.Behaviours
{
    public class Health
    {
        private readonly float _maxHealth;
        
        private PoolableObject _poolableObject;

        private float _currentHealth;

        public Health(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
        }

        public void Init(PoolableObject poolable)
        {
            _currentHealth = _maxHealth;
            _poolableObject = poolable;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

            if (_currentHealth <= 0)
            {
                _poolableObject.Disable();
            }
        }
    }
}