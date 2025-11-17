using MiniIT.Behaviours;

namespace MiniIT.Data
{
    public class BulletData
    {
        public float Damage { get; private set; }
        public IMovable Movable { get; private set; }
        
        public BulletData(float damage, IMovable movable)
        {
            Damage = damage;
            Movable = movable;
        }
    }
}
