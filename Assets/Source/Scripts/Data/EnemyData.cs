using MiniIT.Behaviours;
using MiniIT.Factory;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Data
{
    public class EnemyData
    {
        private readonly EnemyView _view;
        public Health Health { get; private set; } = null;
        public IMovable Movable { get; private set; } = null;
        public IDamageable Damageable => _view;
        public Transform Transform => _view.transform;

        public EnemyData(Health health, EnemyView view, IMovable movable)
        {
            Health = health;
            Movable = movable;
            _view = view;
        }

        public void Init(PoolableObject poolableObject)
        {
            Health.Init(poolableObject);
        }
    }
}