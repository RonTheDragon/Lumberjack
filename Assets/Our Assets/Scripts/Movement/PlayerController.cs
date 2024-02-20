using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement => GetComponent<PlayerMovement>();
    private PlayerRotation _playerRotation => GetComponent<PlayerRotation>();
    private PlayerCameraControl _playerCameraControl => GetComponent<PlayerCameraControl>();

    [SerializeField] private Transform _playerModel;

    [SerializeField] private float _rotationOffset = 45f;

    private Vector2 _moveInput;

    private Vector3 moveDirection;

    private void Update()
    {
        CalculateMoveDirectionWithOffset();

        _playerMovement.Move(moveDirection);
        _playerRotation.RotateToFaceDirection(_playerModel, moveDirection);
        _playerCameraControl.MoveCamera(_playerModel);
    }

    private void CalculateMoveDirectionWithOffset()
    {
        moveDirection = new Vector3(-_moveInput.x, 0f, _moveInput.y).normalized;
        moveDirection = Quaternion.Euler(0, _rotationOffset, 0) * moveDirection;
    }

    public void OnMoveForward(InputAction.CallbackContext context)
    {
        _moveInput.x = context.ReadValue<float>();
    }

    public void OnMoveSideways(InputAction.CallbackContext context)
    {
        _moveInput.y = context.ReadValue<float>();
    }
}
