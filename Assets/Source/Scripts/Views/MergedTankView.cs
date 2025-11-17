using System;
using MiniIT.Factory;
using UnityEngine;

namespace MiniIT.Views
{
    public class MergedTankView : PoolableObject, IDrageable, IMergeable
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private SpriteRenderer _renderer;
        [field: SerializeField] public Collider2D Collider { get; private set; }
        
        private Vector3 _startPosition;
        private Vector3 _offset;

        public Transform Transform { get; private set; } = null;
        public Func<IMergeable, IMergeable, bool> Merging { get; set; }

        private void Awake()
        {
            Transform = transform;
        }

        public void Configure(Sprite sprite)
        {
            _renderer.sprite = sprite;
        }

        public void StartDrag(Vector3 pointerPosition)
        {
            _startPosition = transform.position;
            _offset = transform.position - pointerPosition;
            Collider.enabled = false;
        }

        public void ProcessDrag(Vector3 pointerPosition)
        {
            transform.position = pointerPosition + _offset;
        }

        public void EndDrag()
        {
            CheckOtherColliders();
        }

        protected override void OnDisabled()
        {
            ResetToDefault();
        }

        private void CheckOtherColliders()
        {
            Vector3 pointerPosition = Collider.bounds.center;

            RaycastHit2D hit = Physics2D.Raycast(pointerPosition, Vector2.zero, _layerMask);

            if (hit.collider != null && hit.collider.TryGetComponent(out IMergeable mergeable))
            {
                Collider.enabled = false;
                bool? mergeSuccess = Merging?.Invoke(this, mergeable);

                if (mergeSuccess == false)
                {
                    ResetToDefault();
                }
            }
            else
            {
                ResetToDefault();
            }
        }

        private void ResetToDefault()
        {
            Collider.enabled = true;
            transform.position = _startPosition;
        }
    }
}