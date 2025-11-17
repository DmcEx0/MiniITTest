using MiniIT.Behaviours;
using MiniIT.Data;
using MiniIT.Factory;
using MiniIT.Models;
using UnityEngine;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class TanksController : IInitializable, IStartable, ITickable
    {
        private readonly GridModel _gridModel = null;
        private readonly BulletFactory _bulletFactory = null;
        private readonly MovementSystem _movementSystem = null;

        public TanksController(GridModel gridModel, BulletFactory bulletFactory)
        {
            _gridModel = gridModel;
            _bulletFactory = bulletFactory;
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

                if (tankData.CheckCanFire())
                {
                    BulletData data = _bulletFactory.Get(tankData.TankMerged.Transform.position, tankData.Damage);
                    
                    _movementSystem.AddMovable(data.Movable);
                }
            }
        }
    }
}