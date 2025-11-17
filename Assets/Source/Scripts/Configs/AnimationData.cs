using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.Configs
{
    [Serializable]
    public class AnimationData
    {
        [field: SerializeField] public BehaviourType Type;
        [field: SerializeField] public float Duration { get; private set; }

        [field: Space]
        [SerializeField] private List<AnimationCurve> _curves;
        
        public IReadOnlyList<AnimationCurve> Curves => _curves;
    }
}