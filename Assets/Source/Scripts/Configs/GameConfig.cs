using UnityEngine;

namespace MiniIT.MergeTwo.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float DelayBetweenSpawnNextTank { get; private set; }
        [field: SerializeField] public int InitialTankCount { get; private set; }
        [field: SerializeField] public int EnemyPoolCount { get; private set; }
        [field: SerializeField] public float DelayBetweenSpawnEnemy { get; private set; }
    }
}