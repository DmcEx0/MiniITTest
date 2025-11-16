using MiniIT.Configs;
using MiniIT.Models;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class GameController : IStartable
    {
        private readonly GameConfig _gameConfig;
        private readonly MergedTankConfig _mergedTankConfig;

        private readonly TanksModel _tanksModel;
        private readonly GameFieldModel _gameFieldModel;
        private readonly GridModel _gridModel;

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
    }
}