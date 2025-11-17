using MiniIT.Behaviours;
using MiniIT.Views;

namespace MiniIT.Data
{
    public class EnemyData
    {
        public Health Health { get; private set; }
        public IMovable Movable { get; private set; }

        public EnemyData(Health health, EnemyView view, IMovable movable)
        {
            Health = health;
            Movable = movable;
        }

        public void Init()
        {
            Health.Reset();
        }
    }
}