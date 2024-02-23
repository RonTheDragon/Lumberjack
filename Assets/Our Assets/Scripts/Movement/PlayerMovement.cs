using System;
using UnityEngine;

public class PlayerMovement : Movement
{
    private CharacterController _characterController => GetComponent<CharacterController>();

    protected new void Update()
    {
        base.Update();
        Gravity();
    }

    public void Move(Vector3 moveInput)
    {
        _characterController.Move(moveInput * _moveSpeed * Time.deltaTime);
    }

    private void Gravity()
    {
        _characterController.Move(Vector3.down);
    }
}
