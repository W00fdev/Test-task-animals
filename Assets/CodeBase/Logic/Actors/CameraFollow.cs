using Assets.CodeBase.Infrastructure;
using UnityEngine;

namespace Assets.CodeBase.Logic.Actors
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform Target;
        [SerializeField] private float _mouseSpeed;

        [Header("Set values to offset camera from character")]
        public float RotationAngleX;
        public float RotationAngleY;
        public float MaxRotationAngleX;

        public float Distance;
        public float OffsetY;

        private readonly InputService _inputService = InputService.Instance;

        private void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            if (Target == null)
                return;

            RotateAroundTarget();
            FollowTarget();
        }

        public void SetTarget(Transform cameraTarget)
            => Target = cameraTarget;

        private void RotateAroundTarget()
        {
            Vector3 mouseMovement = _inputService.GetMouseMovement();
            RotationAngleX = Mathf.Clamp(RotationAngleX - mouseMovement.y * _mouseSpeed, -MaxRotationAngleX, MaxRotationAngleX);
            RotationAngleY += mouseMovement.x * _mouseSpeed;
        }

        private void FollowTarget()
        {
            Quaternion rotation = Quaternion.Euler(RotationAngleX, RotationAngleY, 0f);
            Vector3 position = rotation * new Vector3(0f, 0f, -Distance) + FollowingPointPosition();

            transform.SetPositionAndRotation(position, rotation);
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 position = Target.position;
            position.y += OffsetY;

            return position;
        }
    }
}
