using MiniIT.Behaviours;

namespace MiniIT.Data
{
    public class BulletData
    {
        public float Damage { get; private set; }
        public IMovable Movable { get; private set; }
        
        public BulletData(IMovable movable)
        {
            Movable = movable;
        }

        public void Init(float damage)
        {
            Damage = damage;
        }
    }
}
