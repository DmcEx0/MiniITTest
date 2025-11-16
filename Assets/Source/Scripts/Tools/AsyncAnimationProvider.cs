using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MiniIT.Configs;
using UnityEngine;

namespace MiniIT.Tools
{
    public class AsyncAnimationProvider
    {
        private const float OffsetX = 0.4f;
        
        private readonly AnimationsConfig _animationsConfig;

        public AsyncAnimationProvider(AnimationsConfig animationsConfig)
        {
            _animationsConfig = animationsConfig;
        }

        public async UniTask CallBounceEffectAsync(Transform transform, AnimationsType type, CancellationToken token)
        {
            var currentData = _animationsConfig.GetAnimationData(type);

            await transform.DOScale(transform.localScale.x + 0.1f, currentData.Duration)
                .SetEase(currentData.CurveStep1)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }
        
        public async UniTask CallInBounceEffectAsync(Transform target, AnimationsType type,
            DirectionType directionType, float centerX , CancellationToken token)
        {
            var data = _animationsConfig.GetAnimationData(type);
            float duration = data.Duration * 0.5f;

            float xOffset = directionType == DirectionType.Left ? -OffsetX : OffsetX;
            await target.DOMoveX(target.position.x + xOffset, duration)
                .SetEase(data.CurveStep1)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);

            await target.DOMoveX(centerX, duration)
                .SetEase(data.CurveStep2)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }

        // public async UniTask CallFadeInEffectImageAsync(Image image, AnimationsType type, CancellationToken token)
        // {
        //     var currentData = _animationsConfig.GetAnimationData(type);
        //
        //     await image.DOFade(GetTargetAndFromValues(currentData).Item1, currentData.Duration)
        //         .From(GetTargetAndFromValues(currentData).Item2)
        //         .AwaitForComplete(TweenCancelBehaviour.Complete, token);
        // }
        //
        // public async UniTask CallFadeInEffectTextAsync(TMP_Text text, AnimationsType type, CancellationToken token)
        // {
        //     var currentData = _animationsConfig.GetAnimationData(type);
        //
        //     await text.DOFade(GetTargetAndFromValues(currentData).Item1, currentData.Duration)
        //         .From(GetTargetAndFromValues(currentData).Item2)
        //         .AwaitForComplete(TweenCancelBehaviour.Complete, token);
        // }

        // private (float, float) GetTargetAndFromValues(AnimationData currentData)
        // {
        //     int keysAmount = currentData.AnimationCurve.keys.Length;
        //
        //     float targetValue = currentData.AnimationCurve.keys[keysAmount - 1].value;
        //     float fromValue = currentData.AnimationCurve.keys[0].value;
        //
        //     return (targetValue, fromValue);
        // }
    }
}