using MiniIT.MergeTwo.Behaviours;
using MiniIT.MergeTwo.Data;
using MiniIT.MergeTwo.Factory;
using MiniIT.MergeTwo.Models;
using UnityEngine;
using VContainer.Unity;

namespace MiniIT.MergeTwo.Controllers
{
    public class TanksController : IInitializable, IStartable, ITickable
    {
        private readonly GridModel _gridModel = null;
        private readonly BulletFactory _bulletFactory = null;
        private readonly MovementSystem _movementSystem = null;
        private readonly BulletModel _bulletModel = null;

        public TanksController(GridModel gridModel, BulletFactory bulletFactory, BulletModel bulletModel)
        {
            _gridModel = gridModel;
            _bulletFactory = bulletFactory;
            _bulletModel = bulletModel;
            _movementSystem = new MovementSystem();
        }

        public void Initialize()
        {
            _bulletFactory.Prepare();
        }

        public void Start()
        {
        }

        public void Tick()
        {
            _movementSystem.UpdateMovements();

            foreach (var cellData in _gridModel.CellsData)
            {
                MergedTankData tankData = cellData.TankData;

                if (tankData == null)
                {
                    continue;
                }

                tankData.AccumulatedTime += Time.deltaTime;

#if ENABLE_DEBUG
                Debug.Log("Bullet Data Count: " + _bulletModel.BulletData.Count);
#endif
                if (tankData.CheckCanFire())
                {
                    if (_bulletModel.TryGetAvailableData(out BulletData bulletData))
                    {
                        _bulletFactory.GetOnlyView(tankData.TankMerged.Transform.position);

                        bulletData.Init(tankData.Damage);
                        return;
                    }

                    BulletData data = _bulletFactory.Get(tankData.TankMerged.Transform.position, tankData.Damage);

                    _bulletModel.AddData(data);

                    _movementSystem.AddMovable(data.Movable);
                }
            }
        }
    }
}