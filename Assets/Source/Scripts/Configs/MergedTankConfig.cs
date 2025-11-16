using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(menuName = "Configs/MergedTankConfig", fileName = "MergedTankConfig")]
    public class MergedTankConfig : ScriptableObject
    {
        [SerializeField] private List<DefaultMergedTankData> _mergedTanksData;
        
        public IReadOnlyList<DefaultMergedTankData> MergedTanksData => _mergedTanksData;
    }
}