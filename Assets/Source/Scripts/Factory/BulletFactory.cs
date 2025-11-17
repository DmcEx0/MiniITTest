using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Factory
{
    public class BulletFactory : GameObjectFactory
    {
        private readonly MergedTankConfig _config = null;
        private readonly ObjectPool<BulletView> _pool = null;

        public BulletFactory(MergedTankConfig config, Transform bulletContainer)
        {
            _config = config;
            _pool = new ObjectPool<BulletView>(_config.BulletPrefab, 50, bulletContainer, Create);
        }

        public override void Prepare()
        {
            _pool.Initialize();
        }

        public BulletData Get(Vector2 position, float damage)
        {
            BulletView view = _pool.Get();

            view.transform.position = position;

            MovableData movableData = new MovableData(view.Rigidbody, Vector2.up, _config.BulletSpeed);

            BulletData data = new BulletData(movableData, view);
            data.Init(damage);
            
            return data;
        }

        public BulletView GetOnlyView(Vector2 position)
        {
            BulletView view = _pool.Get();

            view.transform.position = position;
            
            return view;
        }
    }
}