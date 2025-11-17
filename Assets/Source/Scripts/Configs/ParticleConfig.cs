using MiniIT.MergeTwo.Views;
using UnityEngine;

namespace MiniIT.MergeTwo.Configs
{
    [CreateAssetMenu(menuName = "Configs/ParticleConfig",  fileName = "ParticleConfig")]
    public class ParticleConfig : ScriptableObject
    {
        [field: SerializeField] public ParticleView StarsParticlePrefab { get; set; }
    }
}
