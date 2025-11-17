using MiniIT.Behaviours;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Data
{
    public class BulletData
    {
        private readonly BulletView _view;
        
        public float Damage { get; private set; }
        public IMovable Movable { get; private set; } = null;
        
        public Transform Transform => _view.transform;
        
        public BulletData(IMovable movable, BulletView view)
        {
            Movable = movable;
            _view = view;
        }

        public void Init(float damage)
        {
            Damage = damage;
        }
    }
}
