using MiniIT.Factory;
using UnityEngine;

namespace MiniIT
{
    public class BulletView : PoolableObject
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    }
}