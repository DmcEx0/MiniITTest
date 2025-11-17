using System;
using MiniIT.Views;

namespace MiniIT
{
    public interface IDamageable
    {
        public Action<BulletView, EnemyView> DamageReceived { get; set; }
    }
}
