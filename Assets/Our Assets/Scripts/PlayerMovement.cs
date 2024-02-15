using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f; // Adjust this value to change movement speed
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _walkingRotation = 45f;
    [SerializeField] private Transform _playerModel;
    private CharacterController _characterController => GetComponentInChildren<CharacterController>();
    private Vector2 _moveInput;

    private void Update()
    {
        // Move the player based on input
        Vector3 moveDirection = new Vector3(-_moveInput.x, 0f, _moveInput.y).normalized;
        moveDirection = Quaternion.Euler(0, _walkingRotation, 0) * moveDirection; // Rotate the movement direction by 45 degrees
        _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);

        // Rotate the player model towards the walking direction
        if (_moveInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
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
