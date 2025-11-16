using System;
using UnityEngine;

namespace MiniIT.Configs
{
    [Serializable]
    public class MergedTankData
    {
        [field: SerializeField] public Sprite CarSprite { get; private set; }
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}
