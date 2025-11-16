using System;
using UnityEngine;

namespace MiniIT.Views
{
    public class MergedTankView : MonoBehaviour, IClickable
    {
        private Vector3 _offset;
        private float _zCoord;
        
        private Vector2 _startPosition;
        private bool _canMerge = false;

        public Action<MergedTankView> Clicked { get; set; }
        
        private void OnMouseDown()
        {
            _zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
            _offset = transform.position - GetMouseWorldPos();
            _startPosition =  transform.position;
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseWorldPos() + _offset;
        }

        private void OnMouseUp()
        {
            transform.position = _startPosition;
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _zCoord;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        public void Click()
        {
            Clicked?.Invoke(this);
        }
    }
}
