using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(menuName = "Configs/MergedTankConfig", fileName = "MergedTankConfig")]
    public class MergedTankConfig : ScriptableObject
    {
        [SerializeField] private List<MergedTankData> _mergedTanksData;
        
        public IReadOnlyList<MergedTankData> MergedTanksData => _mergedTanksData;
    }
}