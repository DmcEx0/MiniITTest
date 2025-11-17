using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MiniIT.Configs;
using MiniIT.Tools;
using UnityEngine;

namespace MiniIT.Providers
{
    public class AsyncAnimationProvider
    {
        private const float OffsetX = 0.4f;

        private readonly AnimationsConfig _animationsConfig = null;

        public AsyncAnimationProvider(AnimationsConfig animationsConfig)
        {
            _animationsConfig = animationsConfig;
        }

        public async UniTask CallMergeEffectAsync(Transform target, BehaviourType type,
            DirectionType directionType, float centerX, CancellationToken token)
        {
            AnimationData data = _animationsConfig.GetAnimationData(type);
            float duration = data.Duration * 0.5f;

            float xOffset = directionType == DirectionType.Left ? -OffsetX : OffsetX;
            await target.DOMoveX(target.position.x + xOffset, duration)
                .SetEase(data.CurveStep1)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);

            await target.DOMoveX(centerX, duration)
                .SetEase(data.CurveStep2)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }

        public async UniTask CallMoveEffectAsync(Transform target, BehaviourType type, CancellationToken token)
        {
            AnimationData data = _animationsConfig.GetAnimationData(type);
            
            await target.DOScaleY(target.localScale.y + 0.2f, data.Duration).SetEase(data.CurveStep1)
                .SetLoops(-1, LoopType.Yoyo)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }
    }
}