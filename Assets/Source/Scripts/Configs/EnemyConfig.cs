using MiniIT.MergeTwo.Views;
using UnityEngine;

namespace MiniIT.MergeTwo.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyView Prefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}