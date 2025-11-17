using System;
using MiniIT.Factory;
using UnityEngine;

namespace MiniIT.Views
{
    public class EnemyView : PoolableObject
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        [SerializeField] private SpriteRenderer _renderer = null;

        private Vector2 _initialPosition;
        
        public void SetInitialPosition(Vector2 initialPosition)
        {
            _initialPosition = initialPosition;
        }
        
        public void FlipSprite(bool flip)
        {
            _renderer.flipY = flip;
        }

        protected override void OnDisabled()
        {
            transform.position = _initialPosition;
        }
    }
}