using System;
using UnityEngine;

namespace MiniIT.Views
{
    public interface IDrageable
    {
        public void StartDrag(Vector3 pointerPosition);
        public void ProcessDrag(Vector3 pointerPosition);
        public void EndDrag();
    }
}
