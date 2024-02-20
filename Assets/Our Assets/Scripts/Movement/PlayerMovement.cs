using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _walkingRotation = 45f;
    [SerializeField] private Transform _playerModel;
    private CharacterController _characterController => GetComponentInChildren<CharacterController>();
    private Vector2 _moveInput;
    private Vector3 _moveDirection;

    protected new void Update()
    {
        base.Update();
        MoveWithInput();
        RotateToFaceMovement();
    }

    private void MoveWithInput()
    {
        // Move the player based on input
        _moveDirection = new Vector3(-_moveInput.x, 0f, _moveInput.y).normalized;
        _moveDirection = Quaternion.Euler(0, _walkingRotation, 0) * _moveDirection; // Rotate the movement direction by 45 degrees
        _characterController.Move(_moveDirection * _moveSpeed * Time.deltaTime);
    }

    private void RotateToFaceMovement()
    {
        // Rotate the player model towards the walking direction
        if (_moveInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
            _playerModel.rotation = Quaternion.Slerp(_playerModel.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    public void OnMoveForward(InputAction.CallbackContext context)
    {
        // Get the movement input from the Input System
        _moveInput.x = context.ReadValue<float>();
    }

    public void OnMoveSideways(InputAction.CallbackContext context)
    {
        // Get the movement input from the Input System
        _moveInput.y = context.ReadValue<float>();
    }
}
