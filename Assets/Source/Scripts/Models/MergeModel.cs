using Cysharp.Threading.Tasks;
using MiniIT.Data;

namespace MiniIT.Models
{
    public class MergeModel
    {
        private readonly AsyncReactiveProperty<MergedTankData> _mergedTankData;

        public IReadOnlyAsyncReactiveProperty<MergedTankData> MergedTankData => _mergedTankData;

        public MergeModel()
        {
            _mergedTankData = new AsyncReactiveProperty<MergedTankData>(null);
        }

        public void RegisterNewData(MergedTankData mergedTankData)
        {
            _mergedTankData.Value = mergedTankData;
        }
    }
}