using MiniIT.MergeTwo.Behaviours;
using UnityEngine;

namespace MiniIT.MergeTwo.Data
{
    public class MovableData : IMovable
    {
        public Rigidbody2D Rigidbody { get; private set; } = null;
        public Vector2 Direction { get; private set; }
        public float Speed { get; private set; }
        
        public MovableData(Rigidbody2D rigidbody, Vector2 direction, float speed)
        {
            Rigidbody = rigidbody;
            Direction = direction;
            Speed = speed;
        }
    }
}