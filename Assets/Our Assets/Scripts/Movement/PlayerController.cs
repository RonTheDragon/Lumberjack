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

    private Vector2 _lookInput;

    private Vector3 moveDirection;

    private Vector3 lookDirection;

    private void Update()
    {
        CalculateMoveDirectionWithOffset();
        CalculateLookDirectionWithOffset();

        _playerMovement.Move(moveDirection);
        _playerRotation.RotateToFaceDirection(_playerModel, lookDirection);
        _playerCameraControl.MoveCamera(_playerModel);
    }

    private void CalculateMoveDirectionWithOffset()
    {
        moveDirection = new Vector3(-_moveInput.x, 0f, _moveInput.y).normalized;
        moveDirection = Quaternion.Euler(0, _rotationOffset, 0) * moveDirection;
    }

    private void CalculateLookDirectionWithOffset()
    {
        lookDirection = new Vector3(-_lookInput.x, 0f, _lookInput.y);
        //lookDirection = Quaternion.Euler(0, _rotationOffset, 0) * lookDirection;
    }

    public void OnMoveForward(InputAction.CallbackContext context)
    {
        _moveInput.x = context.ReadValue<float>();
    }

    public void OnMoveSideways(InputAction.CallbackContext context)
    {
        _moveInput.y = context.ReadValue<float>();
    }

    public void OnLookUpDown(InputAction.CallbackContext context)
    {
        _lookInput.x = context.ReadValue<float>();
    }

    public void OnLookSideways(InputAction.CallbackContext context)
    {
        _lookInput.y = context.ReadValue<float>();
    }
}
