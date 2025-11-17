using System;
using MiniIT.MergeTwo.Views;

namespace MiniIT.MergeTwo.Behaviours
{
    public interface IDamageable
    {
        public Action<BulletView, EnemyView> DamageReceived { get; set; }
    }
}
