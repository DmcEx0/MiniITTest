using Unity.VisualScripting;

namespace MiniIT.Controllers
{
    public class GameController : IInitializable
    {
        private readonly TanksModel _tanksModel;
        private readonly GameFieldModel _gameFieldModel;

        public GameController(TanksModel tanksModel, GameFieldModel gameFieldModel)
        {
            _tanksModel = tanksModel;
            _gameFieldModel = gameFieldModel;
        }

        public void Initialize()
        {
            
        }
    }
}
