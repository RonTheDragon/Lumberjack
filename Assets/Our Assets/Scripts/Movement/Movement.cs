using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 10f;

    private float _originalMoveSpeed;
    private float _currentDuration;

    protected virtual void Update()
    {
        HandleMovementModification();
    }

    private void HandleMovementModification()
    {
        if (_currentDuration > 0f)
        {
            _currentDuration -= Time.deltaTime;

            if (_currentDuration <= 0f)
            {
                RevertMovementModify();
            }
        }
    }

    public void MovementModify(float multiplier, float duration)
    {
        if (duration <= 0f || Mathf.Approximately(multiplier, 0f))
            return;

        if (_originalMoveSpeed == 0f)
            _originalMoveSpeed = _moveSpeed;

        _moveSpeed *= multiplier;

        _currentDuration = duration;

        OnMovementSpeedChanged();
    }

    protected virtual void OnMovementSpeedChanged()
    {
        // Placeholder method to be overridden in subclasses
    }

    private void RevertMovementModify()
    {
        _moveSpeed = _originalMoveSpeed;
        _originalMoveSpeed = 0f;
        _currentDuration = 0f;
        OnMovementSpeedChanged();
    }
}
