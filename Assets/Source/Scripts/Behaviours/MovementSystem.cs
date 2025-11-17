using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.MergeTwo.Behaviours
{
    public class MovementSystem
    {
        private readonly List<IMovable> _movables = null;

        public MovementSystem()
        {
            _movables = new List<IMovable>();
        }

        public void UpdateMovements()
        {
            foreach (var movable in _movables)
            {
                movable.Rigidbody.MovePosition(movable.Rigidbody.position +
                                               movable.Direction * movable.Speed * Time.fixedDeltaTime);
            }
        }

        public void AddMovable(IMovable movement)
        {
            _movables.Add(movement);
        }
    }
}