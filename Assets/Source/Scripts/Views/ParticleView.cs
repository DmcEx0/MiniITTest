using MiniIT.MergeTwo.Factory;
using UnityEngine;

namespace MiniIT.MergeTwo.Views
{
    public class ParticleView : PoolableObject
    {
        [field: SerializeField] public ParticleSystem Particle { get; private set; }
    }
}
