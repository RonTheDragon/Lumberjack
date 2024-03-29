using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement => GetComponent<PlayerMovement>();
    private PlayerCrosshair _playerCrosshair => GetComponent<PlayerCrosshair>();
    private PlayerRotation _playerRotation => GetComponent<PlayerRotation>();
    private PlayerCameraControl _playerCameraControl => GetComponent<PlayerCameraControl>();
    private PlayerAttackManager _playerAttackManager => GetComponent<PlayerAttackManager>();    
    
    [SerializeField] private Transform _playerModel;

    [SerializeField] private float _rotationOffset = 45f;

    private Vector2 _moveInput;

    private Vector2 _lookInput;

    private Vector3 moveDirection;

    private Vector3 lookDirection;

    private void Update()
    {
        CalculateMoveDirectionWithOffset();

        _playerMovement.Move(moveDirection);
        _playerCrosshair.Move(_lookInput);

        CalculateRotationWithOffset();

        _playerRotation.RotateToFaceDirection(_playerModel, lookDirection);

        _playerCameraControl.MoveCamera(_playerModel);
    }

    private void CalculateMoveDirectionWithOffset()
    {
        moveDirection = Quaternion.Euler(0, _rotationOffset, 0) * new Vector3(-_moveInput.x, 0f, _moveInput.y).normalized;
    }

    private void CalculateRotationWithOffset()
    {
        lookDirection = Quaternion.Euler(0, 0, _rotationOffset) * _playerCrosshair.GetCrosshairDirection(); ;
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
        _lookInput.y = -context.ReadValue<float>();
    }

    public void OnLookSideways(InputAction.CallbackContext context)
    {
        _lookInput.x = context.ReadValue<float>();
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _playerAttackManager.OnMeleePress();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _playerAttackManager.OnMeleeRelease();
        }
    }

    public void OnRangeAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _playerAttackManager.OnRangedPress();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _playerAttackManager.OnRangedRelease();
        }
    }
}
