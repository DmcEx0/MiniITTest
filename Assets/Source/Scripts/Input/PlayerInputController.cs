using System;
using MiniIT.Input;
using MiniIT.MergeTwo.Views;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace MiniIT.MergeTwo.Input
{
    public class PlayerInputController : IInitializable, ITickable, IDisposable
    {
        private readonly UserInput _userInput = null;

        private IDrageable _findedDrageable = null;

        private bool _isProcess = false;

        public PlayerInputController()
        {
            _userInput = new UserInput();
        }

        public void Initialize()
        {
            _userInput.Enable();

            _userInput.Player.Click.started += StartDrag;
            _userInput.Player.Click.performed += ProcessDrag;
            _userInput.Player.Click.canceled += EndDrag;
        }

        public void Tick()
        {
            if (_isProcess == false || _findedDrageable == null)
            {
                return;
            }

            _findedDrageable.ProcessDrag(GetMouseWorldPos());
        }

        public void Dispose()
        {
            _userInput.Player.Click.started -= StartDrag;
            _userInput.Player.Click.performed -= ProcessDrag;
            _userInput.Player.Click.canceled -= EndDrag;

            _userInput.Disable();
        }

        private void StartDrag(InputAction.CallbackContext ctx)
        {
            FindDrageable();

            if (_findedDrageable == null)
            {
                return;
            }

            _findedDrageable.StartDrag(GetMouseWorldPos());
        }

        private void ProcessDrag(InputAction.CallbackContext ctx)
        {
            if (_findedDrageable == null)
            {
                return;
            }

            _isProcess = true;
        }

        private void EndDrag(InputAction.CallbackContext ctx)
        {
            if (_findedDrageable == null)
            {
                return;
            }

            _isProcess = false;

            _findedDrageable.EndDrag();
            _findedDrageable = null;
        }

        private void FindDrageable()
        {
            var pointerPosition = _userInput.Player.Drag.ReadValue<Vector2>();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pointerPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out IDrageable drageable))
            {
                _findedDrageable = drageable;
            }
            else
            {
                _findedDrageable = null;
            }
        }


        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = _userInput.Player.Drag.ReadValue<Vector2>();

            mousePoint.z = 0f;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}