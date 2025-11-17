using UnityEngine;

namespace MiniIT.MergeTwo.Behaviours
{
    public interface IMovable
    {
        public Rigidbody2D Rigidbody { get; }
        public Vector2 Direction { get; }
        public float Speed { get; }
    }
}
