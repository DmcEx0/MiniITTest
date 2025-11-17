using UnityEngine;

namespace MiniIT.MergeTwo.Views
{
    public interface IDrageable
    {
        public void StartDrag(Vector3 pointerPosition);
        public void ProcessDrag(Vector3 pointerPosition);
        public void EndDrag();
    }
}
