using System;
using UnityEngine;

namespace MiniIT.Behaviours
{
    public class Health
    {
        private readonly float _maxHealth;

        private float _currentHealth;

        public Action Died;
        
        public Health(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

            if (_currentHealth <= 0)
            {
                Died?.Invoke();
            }
        }
    }
}
