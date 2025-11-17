using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Models;
using MiniIT.Providers;
using MiniIT.Tools;
using MiniIT.Views;
using UnityEngine;
using VContainer.Unity;

namespace MiniIT.Controllers
{
    public class MergeController : IInitializable, IDisposable
    {
        private readonly AsyncAnimationProvider _animationProvider = null;
        private readonly GridModel _gridModel = null;
        private readonly MergeModel _mergeModel = null;
        private readonly ParticleProvider _particleProvider = null;

        private readonly CancellationTokenSource _tokenSource = null;

        public MergeController(GridModel gridModel, MergeModel mergeModel, AsyncAnimationProvider animationProvider,
            ParticleProvider articleProvider)
        {
            _gridModel = gridModel;
            _mergeModel = mergeModel;
            _animationProvider = animationProvider;
            _particleProvider = articleProvider;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _mergeModel.MergedTankData.Subscribe(OnRegisterNewData);
            _particleProvider.Initialize();
        }
        
        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
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

            Vector3 center =
                (firstTankData.TankMerged.Transform.position + secondTankData.TankMerged.Transform.position) * 0.5f;

            firstTankData.TankMerged.Collider.enabled = false;
            secondTankData.TankMerged.Collider.enabled = false;

            UniTask leftTask = _animationProvider.CallMergeEffectAsync(
                firstTankData.TankMerged.Transform,
                BehaviourType.Merge,
                DirectionType.Left,
                center.x,
                _tokenSource.Token
            );

            UniTask rightTask = _animationProvider.CallMergeEffectAsync(
                secondTankData.TankMerged.Transform,
                BehaviourType.Merge,
                DirectionType.Right,
                center.x,
                _tokenSource.Token
            );

            await UniTask.WhenAll(leftTask, rightTask);
            
            _particleProvider.PlayWaitDisableAsync(center).Forget();

            _mergeModel.OnMergedSuccess(firstTankData.Level, secondData);

#if ENABLE_DEBUG
            Debug.Log("[MergeController] Move To: " + secondData.Name);
#endif

            firstData.ReleaseBusyTank();
        }
    }
}