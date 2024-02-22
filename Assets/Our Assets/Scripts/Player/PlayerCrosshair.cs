using UnityEngine;
using UnityEngine.UI;

public class PlayerCrosshair : MonoBehaviour
{
    [SerializeField] private float _maxRadius = 100f; // Maximum radius from the center
    [SerializeField] private float _minRadius = 10f;
    [SerializeField] private Image _crosshair;

    public void Move(Vector3 lookInput)
    {
        if (lookInput != Vector3.zero)
        {
            Vector3 newCrosshairPosition = CalculateNewCrosshairPosition(lookInput);

            // Clamp crosshair position within the maximum radius
            newCrosshairPosition = ClampPositionWithinRadius(newCrosshairPosition);

            UpdateCrosshairPosition(newCrosshairPosition);
        }
    }

    private Vector3 CalculateNewCrosshairPosition(Vector3 lookInput)
    {
        return _crosshair.rectTransform.localPosition + new Vector3(lookInput.x, -lookInput.y, 0) * Time.deltaTime * 100;
    }

    private Vector3 ClampPositionWithinRadius(Vector3 position)
    {
        float distanceFromCenter = position.magnitude;
        if (distanceFromCenter > _maxRadius)
        {
            position = position.normalized * _maxRadius;
        }
        else if (distanceFromCenter < _minRadius)
        {
            position = position.normalized * _minRadius;
        }
        return position;
    }

    private void UpdateCrosshairPosition(Vector3 newPosition)
    {
        _crosshair.rectTransform.localPosition = newPosition;
    }

    public Vector3 GetCrosshairDirection()
    {
        // Convert crosshair position to quaternion rotation
        return  _crosshair.rectTransform.localPosition.normalized;
    }
}
