using System;
using UnityEngine;

namespace MiniIT.Configs
{
    [Serializable]
    public class AnimationData
    {
        [field: SerializeField] public AnimationsType Type;

        [field: Space]
        [field: SerializeField] public AnimationCurve AnimationCurve  { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}