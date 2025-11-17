using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.MergeTwo.Configs
{
    [CreateAssetMenu(fileName = "AnimationsConfig", menuName = "Configs/AnimationsConfig")]
    public class AnimationsConfig : ScriptableObject
    {
        [SerializeField] private List<AnimationData> _animationsData;

        public AnimationData GetAnimationData(BehaviourType behaviourType)
        {
            AnimationData currentData = _animationsData[0];

            foreach (var animationData in _animationsData)
            {
                if (behaviourType == animationData.Type)
                {
                    currentData = animationData;
                }
            }

            return currentData;
        }
    }
}