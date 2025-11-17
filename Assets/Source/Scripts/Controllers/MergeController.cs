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

        private ParticleSystem _particleSystem;

        public MergeController(GridModel gridModel, MergeModel mergeModel, AsyncAnimationProvider animationProvider,
            ParticleSystem particleSystem)
        {
            _gridModel = gridModel;
            _mergeModel = mergeModel;
            _animationProvider = animationProvider;
            _particleSystem = particleSystem;
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

            mergedTankData.TankMerged.Merging += TryMerge;
        }

        private bool TryMerge(IMergeable dragTank, IMergeable secondaryTank)
        {
            CellData firstData = _gridModel.CellsData.Where(data => data.TankData != null)
                .FirstOrDefault(data => ReferenceEquals(data.TankData.TankMerged, dragTank));

            CellData secondData = _gridModel.CellsData.Where(data => data.TankData != null)
                .FirstOrDefault(data => ReferenceEquals(data.TankData.TankMerged, secondaryTank));

            if (firstData == null || secondData == null)
            {
                Debug.LogError($"Merge failed. Fist data is {firstData}. Secondary data is {secondData}");
                return false;
            }

#if ENABLE_DEBUG
            Debug.Log("[MergeController] First Data: " + firstData.Name);
            Debug.Log("[MergeController] Second Data: " + secondData.Name);
#endif

            MergedTankData firstTankData = firstData.TankData;
            MergedTankData secondTankData = secondData.TankData;

            if (firstTankData.Level >= _mergeModel.MaxTankLevel || secondTankData.Level >= _mergeModel.MaxTankLevel)
            {
                return false;
            }

            if (firstTankData.Level == secondTankData.Level)
            {
                dragTank.Merging -= TryMerge;
                secondaryTank.Merging -= TryMerge;
                    
                MergeAsync(firstData, secondData).Forget();

                return true;
            }

            return false;
        }

        private async UniTask MergeAsync(CellData firstData, CellData secondData)
        {
            MergedTankData firstTankData = firstData.TankData;
            MergedTankData secondTankData = secondData.TankData;
            
            firstTankData.TankMerged.Transform.position = secondTankData.TankMerged.Transform.position;

            float center =
                (firstTankData.TankMerged.Transform.position.x + secondTankData.TankMerged.Transform.position.x) * 0.5f;

            firstTankData.TankMerged.Collider.enabled = false;
            secondTankData.TankMerged.Collider.enabled = false;

            UniTask leftTask = _animationProvider.CallInBounceEffectAsync(
                firstTankData.TankMerged.Transform,
                AnimationsType.Merge,
                DirectionType.Left,
                center,
                CancellationToken.None
            );

            UniTask rightTask = _animationProvider.CallInBounceEffectAsync(
                secondTankData.TankMerged.Transform,
                AnimationsType.Merge,
                DirectionType.Right,
                center,
                CancellationToken.None
            );

            await UniTask.WhenAll(leftTask, rightTask);
            
            Vector3 a = (firstTankData.TankMerged.Transform.position + secondTankData.TankMerged.Transform.position) * 0.5f;
            Object.Instantiate(_particleSystem, a, Quaternion.identity);

            _mergeModel.OnMergedSuccess(firstTankData.Level, secondData);

#if ENABLE_DEBUG
            Debug.Log("[MergeController] Move To: " + secondData.Name);
#endif

            firstData.ReleaseBusyTank();
        }
    }
}