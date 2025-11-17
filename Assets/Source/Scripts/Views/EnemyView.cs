using System;
using MiniIT.MergeTwo.Behaviours;
using MiniIT.MergeTwo.Factory;
using UnityEngine;

namespace MiniIT.MergeTwo.Views
{
    public class EnemyView : PoolableObject, IDamageable
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        [SerializeField] private SpriteRenderer _renderer = null;

        private Vector2 _initialPosition;
        
        public Action<BulletView, EnemyView> DamageReceived { get; set; }
        
        public void SetInitialPosition(Vector2 initialPosition)
        {
            _initialPosition = initialPosition;
        }
        
        public void FlipSprite(bool flip)
        {
            _renderer.flipY = flip;
        }

        public void TakeDamage(BulletView  bulletView)
        {
            DamageReceived?.Invoke(bulletView, this);
        }

        protected override void OnDisabled()
        {
            transform.position = _initialPosition;
        }
    }
}