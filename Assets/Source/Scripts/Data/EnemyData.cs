using MiniIT.Behaviours;
using MiniIT.Factory;

namespace MiniIT.Data
{
    public class EnemyData
    {
        public Health Health { get; private set; } = null;
        public IMovable Movable { get; private set; } = null;
        public IDamageable Damageable { get; private set; } = null;

        public EnemyData(Health health, IDamageable damageable, IMovable movable)
        {
            Health = health;
            Movable = movable;
            Damageable = damageable;
        }

        public void Init(PoolableObject poolableObject)
        {
            Health.Init(poolableObject);
        }
    }
}