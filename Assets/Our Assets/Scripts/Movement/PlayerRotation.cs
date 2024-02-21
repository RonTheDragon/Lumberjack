using UnityEngine;
using UnityEngine.UI;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Image _crosshair;
    [SerializeField] private float _maxRadius = 100f; // Maximum radius from the center

    public void RotateToFaceDirection(Transform playerModel, Vector3 moveInput)
    {
        if (moveInput != Vector3.zero)
        {
            // Calculate the new crosshair position based on move input
            Vector3 newCrosshairPosition = _crosshair.rectTransform.localPosition + new Vector3(moveInput.z, -moveInput.x, 0) * Time.deltaTime * 100;

            // Calculate the distance from the center
            float distanceFromCenter = newCrosshairPosition.magnitude;

            // Check if the distance exceeds the maximum radius
            if (distanceFromCenter > _maxRadius)
            {
                // If so, clamp the position to the maximum radius
                newCrosshairPosition = newCrosshairPosition.normalized * _maxRadius;
            }

            // Update the crosshair position
            _crosshair.rectTransform.localPosition = newCrosshairPosition;

            // Calculate target rotation towards the crosshair
            Vector3 lookAtPosition = new Vector3(newCrosshairPosition.x, 0, newCrosshairPosition.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition, Vector3.up);

            // Apply additional rotation (if needed)
            targetRotation = Quaternion.Euler(0, -45, 0) * targetRotation;

            // Smoothly rotate the player towards the target rotation
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
