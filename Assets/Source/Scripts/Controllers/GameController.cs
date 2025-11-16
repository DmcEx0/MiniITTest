using MiniIT.Configs;
using MiniIT.Models;
using UnityEngine;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class GameController : IStartable, ITickable
    {
        private readonly GameConfig _gameConfig;
        private readonly MergedTankConfig _mergedTankConfig;

        private readonly TanksModel _tanksModel;
        private readonly GameFieldModel _gameFieldModel;
        private readonly GridModel _gridModel;
        
        private float _accumulatedTimeForSpawnTank;

        public GameController(GameConfig gameConfig, MergedTankConfig mergedTankConfig, TanksModel tanksModel,
            GameFieldModel gameFieldModel, GridModel gridModel)
        {
            _gameConfig = gameConfig;
            _mergedTankConfig = mergedTankConfig;
            _tanksModel = tanksModel;
            _gameFieldModel = gameFieldModel;
            _gridModel = gridModel;
        }

        public void Start()
        {
            _gridModel.BuildGrid();
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
            var tank = Object.Instantiate(_mergedTankConfig.MergedTanksData[0].TankView);

            var freeCells = _gridModel.GetFreeCells();
            
            var randomCell = freeCells[Random.Range(0, freeCells.Count)];
        }
    }
}