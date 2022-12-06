using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Logic.Actors;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _angleControl;
    [SerializeField] private float _moveSpeed;
    
    private CharacterController _characterController;
    private readonly InputService _inputService = InputService.Instance;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        
        if (Camera.main.transform.TryGetComponent(out CameraFollow cameraFollow))
            cameraFollow.SetTarget(transform);
    }

    private void Update()
    {
        Vector3 inputVector = TranslateCameraDirection(_inputService.GetMovementInput());

        inputVector = ApplyGravity(inputVector);

        MovePlayer(inputVector * _moveSpeed);
        RotatePlayerTowardIfNeeded(inputVector);
    }

    private Vector3 ApplyGravity(Vector3 inputVector)
    {
        inputVector.y = -9.81f;
        return inputVector;
    }

    private void MovePlayer(Vector3 movementVector) 
        => _characterController.Move(movementVector * Time.deltaTime);

    private Vector3 TranslateCameraDirection(Vector3 inputVector)
    {
        if (inputVector != Vector3.zero)
        {
            if (Camera.main == null)
                throw new System.Exception("Do not forget to set Main Camera tag");

            inputVector = Camera.main.transform.TransformDirection(inputVector);
        }

        return inputVector; 
    }

    private void RotatePlayerTowardIfNeeded(Vector3 movementDirection)
    {
        // При повороте на нулевой вектор, LookRotation ругается.
        if (Vector3.Dot(transform.forward, movementDirection) == 0)
            return;

        movementDirection.y = 0f;

        transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(movementDirection), _angleControl * Time.deltaTime);
    }
}
