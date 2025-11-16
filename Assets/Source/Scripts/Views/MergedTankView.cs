using UnityEngine;

namespace MiniIT.Views
{
    public class MergedTankView : MonoBehaviour
    {
        private Vector3 _offset;
        private float _zCoord;

        private void OnMouseDown()
        {
            _zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
            _offset = transform.position - GetMouseWorldPos();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseWorldPos() + _offset;
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _zCoord;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}
