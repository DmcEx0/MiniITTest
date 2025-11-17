using MiniIT.MergeTwo.Views;
using UnityEngine;

namespace MiniIT.MergeTwo.Configs
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Configs/GridConfig")]
    public class GridConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2Int GridSize { get; private set; }
        [field: SerializeField] public CellView CellPrefab { get; private set; }
        [field: SerializeField] public float CellSize { get; private set; }
        [field: SerializeField] public float CellSpacing { get; private set; }
    }
}
