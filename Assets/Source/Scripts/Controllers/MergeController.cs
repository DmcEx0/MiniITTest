using Cysharp.Threading.Tasks.Linq;
using MiniIT.Data;
using MiniIT.Models;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class MergeController : IInitializable
    {
        private readonly GridModel _gridModel;
        private readonly MergeModel _mergeModel;

        public MergeController(GridModel gridModel, MergeModel mergeModel)
        {
            _gridModel = gridModel;
            _mergeModel = mergeModel;
        }

        public void Initialize()
        {
            _mergeModel.MergedTankData.Subscribe(OnRegisterNewData);
        }

        private void OnRegisterNewData(MergedTankData mergedTankData)
        {
        }
    }
}