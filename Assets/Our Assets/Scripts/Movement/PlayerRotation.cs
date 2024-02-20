using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    public void RotateToFaceDirection(Transform playerModel, Vector3 moveInput)
    {
        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
