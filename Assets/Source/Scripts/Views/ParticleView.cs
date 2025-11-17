using MiniIT.Factory;
using UnityEngine;

namespace MiniIT.Views
{
    public class ParticleView : PoolableObject
    {
        [field: SerializeField] public ParticleSystem Particle { get; private set; }
    }
}
