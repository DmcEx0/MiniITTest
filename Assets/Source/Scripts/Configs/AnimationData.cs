using System;
using UnityEngine;

namespace MiniIT.Configs
{
    [Serializable]
    public class AnimationData
    {
        [field: SerializeField] public BehaviourType Type;

        [field: Space]
        [field: SerializeField] public AnimationCurve CurveStep1  { get; private set; }
        [field: SerializeField] public AnimationCurve CurveStep2  { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}