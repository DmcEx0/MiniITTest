using System;
using Cysharp.Threading.Tasks;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Factory;
using MiniIT.Models;
using UnityEngine;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class GameController : IInitializable, IStartable, IDisposable
    {
        private const int FirstTankLevel = 0;

        private readonly GameConfig _gameConfig;
        private readonly TankFactory _tankFactory;

        private readonly MergeModel _mergeModel;
        private readonly GridModel _gridModel;

        private bool _canSpawnTank;

        public GameController(GameConfig gameConfig, GridModel gridModel, TankFactory tankFactory,
            MergeModel mergeModel)
        {
            _gameConfig = gameConfig;
            _gridModel = gridModel;
            _tankFactory = tankFactory;
            _mergeModel = mergeModel;
        }

        public void Initialize()
        {
            _mergeModel.MergedSuccess += SpawnTank;
            _canSpawnTank = true;
        }

        public void Dispose()
        {
            _mergeModel.MergedSuccess -= SpawnTank;
            _canSpawnTank = false;
        }

        public void Start()
        {
            _gridModel.BuildGrid();

            for (int i = 0; i < _gameConfig.InitialTankCount; i++)
            {
                SpawnOnCell();
            }

            SpawnAsync().Forget();
        }

        private async UniTask SpawnAsync()
        {
            while (_canSpawnTank)
            {
                await UniTask.Delay(TimeSpan.FromMilliseconds(_gameConfig.MillisecondsToSpawnNextTank));

                SpawnOnCell();
            }
        }

        private void SpawnOnCell()
        {
            if (_gridModel.TryGetFreeCell(out CellData freeCell) == false)
            {
                return;
            }

            SpawnTank(FirstTankLevel, freeCell);
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