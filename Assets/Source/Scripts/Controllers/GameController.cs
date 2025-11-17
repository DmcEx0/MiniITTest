using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Factory;
using MiniIT.Models;
using MiniIT.Providers;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class GameController : IInitializable, IStartable, IDisposable
    {
        private const int FirstTankLevel = 0;

        private readonly GameConfig _gameConfig = null;
        private readonly TankFactory _tankFactory = null;
        private readonly AsyncAnimationProvider _animationProvider = null;

        private readonly MergeModel _mergeModel = null;
        private readonly GridModel _gridModel = null;

        private bool _canSpawnTank = false;

        public GameController(GameConfig gameConfig, GridModel gridModel, TankFactory tankFactory,
            MergeModel mergeModel, AsyncAnimationProvider animationProvider)
        {
            _gameConfig = gameConfig;
            _gridModel = gridModel;
            _tankFactory = tankFactory;
            _mergeModel = mergeModel;
            _animationProvider = animationProvider;
        }

        public void Initialize()
        {
            _tankFactory.Prepare();

            _mergeModel.MergedSuccess += SpawnTank;
            _mergeModel.MaxTankLevel = _tankFactory.GetMaxTankLevel();

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
                await UniTask.Delay(TimeSpan.FromSeconds(_gameConfig.DelayBetweenSpawnNextTank));

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

            cellData.ChangeTank(newTankData);

            _animationProvider.CallBounceScaleEffectAsync(cellData.TankData.Transform, BehaviourType.None, CancellationToken.None).Forget();

            _mergeModel.RegisterNewData(newTankData);
        }
    }
}