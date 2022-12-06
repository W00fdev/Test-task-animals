using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    // Singletone light input service
    public class InputService
    {
        private static InputService _instance;

        public static InputService Instance => _instance ??= new InputService();

        private Vector3 _inputVector = Vector3.zero;
        private Vector3 _mouseVector = Vector3.zero;

        public Vector3 GetMovementInput()
        {
            _inputVector.x = Input.GetAxis("Horizontal");
            _inputVector.z = Input.GetAxis("Vertical");

            return _inputVector;
        }

        public Vector3 GetMouseMovement()
        {
            _mouseVector.x = Input.GetAxis("Mouse X");
            _mouseVector.y = Input.GetAxis("Mouse Y");

            return _mouseVector;
        }
    }
}

