using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Models;
using MiniIT.Tools;
using MiniIT.Views;
using UnityEngine;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class MergeController : IInitializable
    {
        private readonly AsyncAnimationProvider _animationProvider;
        private readonly GridModel _gridModel;
        private readonly MergeModel _mergeModel;

        public MergeController(GridModel gridModel, MergeModel mergeModel, AsyncAnimationProvider animationProvider)
        {
            _gridModel = gridModel;
            _mergeModel = mergeModel;
            _animationProvider = animationProvider;
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

        private bool TryMerge(IMergeable dragTank, IMergeable secondaryTank)
        {
            var firstData = _gridModel.CellsData.Where(data => data.TankData != null)
                .FirstOrDefault(data => ReferenceEquals(data.TankData.View, dragTank));

            var secondData = _gridModel.CellsData.Where(data => data.TankData != null)
                .FirstOrDefault(data => ReferenceEquals(data.TankData.View, secondaryTank));

            if (firstData == null || secondData == null)
            {
                Debug.LogError($"Merge failed. Fist data is {firstData}. Secondary data is {secondData}");
                return false;
            }

#if ENABLE_DEBUG
            Debug.Log("[MergeController] First Data: " + firstData.Name);
            Debug.Log("[MergeController] Second Data: " + secondData.Name);
#endif

            var firstTankData = firstData.TankData;
            var secondaryTankData = secondData.TankData;

            if (firstTankData.Level == secondaryTankData.Level)
            {
                MergeAsync(dragTank, secondaryTank, firstData, secondData).Forget();

                return true;
            }

            return false;
        }

        //TODO: Refactored IMergeable
        private async UniTask MergeAsync(IMergeable dragTank, IMergeable secondaryTank, CellData firstData,
            CellData secondData)
        {
            dragTank.Merging -= TryMerge;
            secondaryTank.Merging -= TryMerge;

            firstData.TankData.View.transform.position = secondData.TankData.View.transform.position;
            
            var center =
                (firstData.TankData.View.transform.position.x + secondData.TankData.View.transform.position.x) * 0.5f;
            
            var leftTask = _animationProvider.CallInBounceEffectAsync(
                firstData.TankData.View.transform,
                AnimationsType.Merge,
                DirectionType.Left,
                center,
                CancellationToken.None
            );

            var rightTask = _animationProvider.CallInBounceEffectAsync(
                secondData.TankData.View.transform,
                AnimationsType.Merge,
                DirectionType.Right,
                center,
                CancellationToken.None
            );

            await UniTask.WhenAll(leftTask, rightTask);

            _mergeModel.OnMergedSuccess(firstData.TankData.Level, secondData);

#if ENABLE_DEBUG
            Debug.Log("[MergeController] Move To: " + secondData.Name);
#endif

            firstData.ChangeTank(null);
        }
    }
}