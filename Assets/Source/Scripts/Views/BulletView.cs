using MiniIT.MergeTwo.Factory;
using UnityEngine;

namespace MiniIT.MergeTwo.Views
{
    public class BulletView : PoolableObject
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyView enemyView))
            {
                enemyView.TakeDamage(this);
                Disable();
            }
        }
    }
}