using System;
using UnityEngine;

namespace MiniIT.Views
{
    public class MergedTankView : MonoBehaviour, IDrageable
    {
        private Vector3 _offset;
        
        private Vector2 _startPosition;

        public Action<MergedTankView> MergePerformed { get; set; }

        public void StartDrag(Vector3 pointerPosition)
        {
            _offset = transform.position - pointerPosition;
            _startPosition =  transform.position;
        }

        public void ProcessDrag(Vector3 pointerPosition)
        {
            transform.position = pointerPosition + _offset;
        }

        public void EndDrag()
        {
            transform.position = _startPosition;
        }
    }
}
