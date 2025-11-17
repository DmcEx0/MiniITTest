using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float MillisecondsToSpawnNextTank { get; private set; }
        [field: SerializeField] public int InitialTankCount { get; private set; }
        [field: SerializeField] public int MaxEnemyCount { get; private set; }
    }
}