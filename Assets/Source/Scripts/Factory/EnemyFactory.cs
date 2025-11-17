using MiniIT.Behaviours;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Factory
{
    public class EnemyFactory : GameObjectFactory
    {
        private readonly EnemyConfig _enemyConfig = null;

        private readonly ObjectPool<EnemyView> _pool = null;

        public EnemyFactory(EnemyConfig enemyConfig, Transform container, GameConfig gameConfig)
        {
            _enemyConfig = enemyConfig;

            _pool = new ObjectPool<EnemyView>(enemyConfig.Prefab, gameConfig.EnemyPoolCount, container);
        }

        public void Prepare()
        {
            _pool.Initialize();
        }

        public EnemyData Get(Vector3 position)
        {
            EnemyView view = GetOnlyView();
            
            view.transform.position = position;

            view.SetInitialPosition(position);
            
            Vector2 direction = Vector2.right;

            if (position.x > 0)
            {
                view.FlipSprite(true);
                direction = Vector2.left;
            }

            Health health = new Health(_enemyConfig.Health);

            MovableData movableData = new MovableData(view.Rigidbody, direction, _enemyConfig.Speed);

            EnemyData data = new EnemyData(health, view, movableData);
            
            data.Init(view);

            return data;
        }

        public EnemyView GetOnlyView()
        {
            EnemyView view = _pool.Get();
            
            return view;
        }
    }
}