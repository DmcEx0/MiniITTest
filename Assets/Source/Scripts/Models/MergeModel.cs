using System;
using Cysharp.Threading.Tasks;
using MiniIT.Data;

namespace MiniIT.Models
{
    public class MergeModel
    {
        private readonly AsyncReactiveProperty<MergedTankData> _mergedTankData = null;

        public int MaxTankLevel { get; set; }

        public Action<int, CellData> MergedSuccess;

        public IReadOnlyAsyncReactiveProperty<MergedTankData> MergedTankData => _mergedTankData;

        public MergeModel()
        {
            _mergedTankData = new AsyncReactiveProperty<MergedTankData>(null);
        }

        public void RegisterNewData(MergedTankData mergedTankData)
        {
            _mergedTankData.Value = mergedTankData;
        }

        public void OnMergedSuccess(int mergedTanksLevel, CellData cellData)
        {
            mergedTanksLevel++;
            MergedSuccess?.Invoke(mergedTanksLevel, cellData);
        }
    }
}