using System;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Factory;
using MiniIT.Models;
using UnityEngine;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class GameController : IInitializable, IStartable, ITickable, IDisposable
    {
        private readonly GameConfig _gameConfig;
        private readonly TankFactory _tankFactory;

        private readonly TanksModel _tanksModel;
        private readonly MergeModel _mergeModel;
        private readonly GridModel _gridModel;

        private float _accumulatedTimeForSpawnTank;

        public GameController(GameConfig gameConfig, TanksModel tanksModel, GridModel gridModel,
            TankFactory tankFactory, MergeModel mergeModel)
        {
            _gameConfig = gameConfig;
            _tanksModel = tanksModel;
            _gridModel = gridModel;
            _tankFactory = tankFactory;
            _mergeModel = mergeModel;
        }
        
        public void Initialize()
        {
            _mergeModel.MergedSuccess += SpawnTank;
        }
        
        public void Dispose()
        {
            _mergeModel.MergedSuccess -= SpawnTank;
        }

        public void Start()
        {
            _gridModel.BuildGrid();

            for (int i = 0; i < 20; i++)
            {
                if (_gridModel.TryGetFreeCell(out CellData freeCell) == false)
                {
                    return;
                }
                
                SpawnTank(0, freeCell);
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

        private void SpawnTank(int level, CellData cellData)
        {
            if (_tankFactory.TryGet(out MergedTankData newTankData, level, cellData) == false)
            {
                return;
            }

            cellData.IsBusy = true;
            cellData.ChangeTank(newTankData);

            _mergeModel.RegisterNewData(newTankData);
        }
    }
}