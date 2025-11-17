using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MiniIT.MergeTwo.Configs;
using MiniIT.MergeTwo.Tools;
using UnityEngine;

namespace MiniIT.MergeTwo.Providers
{
    public class AsyncAnimationProvider
    {
        private const float OffsetX = 0.4f;

        private readonly AnimationsConfig _animationsConfig = null;

        public AsyncAnimationProvider(AnimationsConfig animationsConfig)
        {
            _animationsConfig = animationsConfig;
        }

        public async UniTask CallBounceMoveXEffectAsync(Transform target, BehaviourType type,
            DirectionType directionType, float centerX, CancellationToken token)
        {
            AnimationData data = _animationsConfig.GetAnimationData(type);
            float duration = data.Duration * 0.5f;

            float xOffset = directionType == DirectionType.Left ? -OffsetX : OffsetX;
            await target.DOMoveX(target.position.x + xOffset, duration)
                .SetEase(data.Curves[0])
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);

            await target.DOMoveX(centerX, duration)
                .SetEase(data.Curves[1])
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }

        public async UniTask CallBounceScaleYEffectAsync(Transform target, BehaviourType type, CancellationToken token)
        {
            float offsetY = 0.2f;
            AnimationData data = _animationsConfig.GetAnimationData(type);
            
            await target.DOScaleY(target.localScale.y + offsetY, data.Duration).SetEase(data.Curves[0])
                .SetLoops(-1, LoopType.Yoyo)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }

        public async UniTask CallBounceScaleEffectAsync(Transform target, BehaviourType type, CancellationToken token)
        {
            AnimationData data = _animationsConfig.GetAnimationData(type);
            
            await target.DOScale(target.localScale, data.Duration)
                .From(Vector3.zero)
                .SetEase(data.Curves[0])
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }
    }
}