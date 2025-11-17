using MiniIT.Views;
using UnityEngine;

namespace MiniIT
{
    [CreateAssetMenu(menuName = "Configs/ParticleConfig",  fileName = "ParticleConfig")]
    public class ParticleConfig : ScriptableObject
    {
        [field: SerializeField] public ParticleView StarsParticlePrefab { get; set; }
    }
}
