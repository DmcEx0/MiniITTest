using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Factory;
using MiniIT.Models;
using UnityEngine;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class GameController : IStartable, ITickable
    {
        private readonly GameConfig _gameConfig;
        private readonly MergedTankConfig _mergedTankConfig;
        private readonly TankFactory _tankFactory;

        private readonly TanksModel _tanksModel;
        private readonly MergeModel _mergeModel;
        private readonly GridModel _gridModel;

        private float _accumulatedTimeForSpawnTank;

        public GameController(GameConfig gameConfig, MergedTankConfig mergedTankConfig, TanksModel tanksModel,
            GridModel gridModel, TankFactory tankFactory, MergeModel mergeModel)
        {
            _gameConfig = gameConfig;
            _mergedTankConfig = mergedTankConfig;
            _tanksModel = tanksModel;
            _gridModel = gridModel;
            _tankFactory = tankFactory;
            _mergeModel = mergeModel;
        }

        public void Start()
        {
            _gridModel.BuildGrid();

            for (int i = 0; i < 5; i++)
            {
                SpawnTank();
            }
        }

        public void Tick()
        {
            _accumulatedTimeForSpawnTank += Time.deltaTime;

            if (_accumulatedTimeForSpawnTank >= _gameConfig.SpawnTankDelay)
            {
                _accumulatedTimeForSpawnTank = 0;
            }
        }

        private void SpawnTank()
        {
            if (_gridModel.TryGetFreeCell(out CellData freeCell) == false)
            {
                return;
            }

            var newTankData = _tankFactory.Get(0, freeCell);
            
            freeCell.IsBusy = true;
            freeCell.AddTank(newTankData);
            
            _mergeModel.RegisterNewData(newTankData);
        }
    }
}