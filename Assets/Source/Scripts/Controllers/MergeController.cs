using System.Linq;
using Cysharp.Threading.Tasks.Linq;
using MiniIT.Data;
using MiniIT.Models;
using MiniIT.Views;
using UnityEngine;
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
            if (mergedTankData == null)
            {
                return;
            }

            mergedTankData.View.Merging += TryMerge;
        }

        private void TryMerge(IMergeable dragTank, IMergeable secondaryTank)
        {
            var firstData = _gridModel.CellsData.Where(data => data.TankData != null)
                .FirstOrDefault(data => ReferenceEquals(data.TankData.View, dragTank));
            
            var secondData = _gridModel.CellsData.Where(data => data.TankData != null)
                    .FirstOrDefault(data => ReferenceEquals(data.TankData.View, secondaryTank));

            if (firstData == null || secondData == null)
            {
                Debug.LogError($"Merge failed. Fist data is {firstData}. Secondary data is {secondData}");
                return;
            }

            if (firstData.TankData.Level == secondData.TankData.Level)
            {
                Debug.Log("Merge successful");
            }
        }
    }
}