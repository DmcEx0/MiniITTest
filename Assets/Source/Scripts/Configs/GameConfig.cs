using UnityEngine;

namespace MiniIT.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnTankDelay { get; private set; }
    }
}
