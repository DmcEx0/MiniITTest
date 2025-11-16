using System;
using UnityEngine;

namespace MiniIT.Views
{
    public class MergedTankView : MonoBehaviour, IDrageable, IMergeable
    {
        private Vector3 _offset;
        private bool _canMerge = false;

        [field: SerializeField] public Collider2D Collider { get; private set; }
        public Action<MergedTankView, MergedTankView> Merging { get; set; }

        public void StartDrag(Vector3 pointerPosition)
        {
            _offset = transform.position - pointerPosition;
            _canMerge = false;
        }

        public void ProcessDrag(Vector3 pointerPosition)
        {
            transform.position = pointerPosition + _offset;
        }

        public void EndDrag()
        {
            _canMerge = true;
            // transform.position = _startPosition;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_canMerge == false)
            {
                return;
            }
            
            if (other.TryGetComponent(out MergedTankView mergedTankView))
            {
                Collider.enabled = false;
                Merging?.Invoke(this, mergedTankView);
            }
        }
    }
}
