using System.Collections.Generic;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(menuName = "Configs/MergedTankConfig", fileName = "MergedTankConfig")]
    public class MergedTankConfig : ScriptableObject
    {
        [field: SerializeField] public MergedTankView MainTankPrefab { get; private set; }
        
        [SerializeField] private List<DefaultMergedTankData> _mergedTanksData;
        
        public IReadOnlyList<DefaultMergedTankData> MergedTanksData => _mergedTanksData;
    }
}