using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private CharacterController _characterController => GetComponent<CharacterController>();

    public void Move(Vector3 moveInput)
    {
        _characterController.Move(moveInput * _moveSpeed * Time.deltaTime);
    }
}
