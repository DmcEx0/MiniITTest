using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(menuName = "Configs/MergedCarConfig", fileName = "MergedCarConfig")]
    public class MergedCarConfig : ScriptableObject
    {
        [SerializeField] private List<MergedCarData> _mergedCarsData;
        
        public IReadOnlyList<MergedCarData> MergedCarsData => _mergedCarsData;
    }
}