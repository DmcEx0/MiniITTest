using MiniIT.Data;
using MiniIT.Models;

namespace MiniIT.Factory
{
    public class TankSpawner
    {
        private readonly MergeModel _mergeModel;
        private readonly TankFactory _tankFactory;

        public TankSpawner(TankFactory tankFactory, MergeModel mergeModel)
        {
            _tankFactory = tankFactory;
            _mergeModel = mergeModel;
        }


    }
}