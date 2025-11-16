using System;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Configs
{
    [Serializable]
    public class DefaultMergedTankData
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}