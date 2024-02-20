using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float _cameraForwardDistance = 5f;
    [SerializeField] private float _cameraSmoothTime = 0.1f;
    private Vector3 _cameraVelocity;

    public void MoveCamera(Transform playerModel)
    {
        Vector3 targetPosition = playerModel.position + playerModel.forward * _cameraForwardDistance;
        _cameraHolder.position = Vector3.SmoothDamp(_cameraHolder.position, targetPosition, ref _cameraVelocity, _cameraSmoothTime);
    }
}
