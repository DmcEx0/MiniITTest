using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(fileName = "AnimationsConfig", menuName = "Configs/AnimationsConfig")]
    public class AnimationsConfig : ScriptableObject
    {
        [SerializeField] private List<AnimationData> _animationsData;

        public AnimationData GetAnimationData(AnimationsType animationsType)
        {
            AnimationData currentData = _animationsData[0];

            foreach (var animationData in _animationsData)
            {
                if (animationsType == animationData.Type)
                {
                    currentData = animationData;
                }
            }

            return currentData;
        }
    }
}