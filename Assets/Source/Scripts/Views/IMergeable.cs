using System;
using UnityEngine;

namespace MiniIT.MergeTwo.Views
{
    public interface IMergeable
    {
        public Transform Transform { get; }
        public Collider2D Collider { get; }
        public Func<IMergeable, IMergeable, bool> Merging { get; set; }
    }
}
