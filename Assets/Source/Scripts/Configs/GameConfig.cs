using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 GridSize { get; private set; }
        [field: SerializeField] public CellView CellPrefab { get; private set; }
        [field: SerializeField] public float CellSize { get; private set; }
        [field: SerializeField] public float CellSpacing { get; private set; }
        [field: SerializeField] public float SpawnTankDelay { get; private set; }
    }
}
