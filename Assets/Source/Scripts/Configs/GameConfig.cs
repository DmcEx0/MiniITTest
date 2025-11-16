using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 GameFieldSize { get; private set; }
        [field: SerializeField] public float GameCellScale { get; private set; }
    }
}
