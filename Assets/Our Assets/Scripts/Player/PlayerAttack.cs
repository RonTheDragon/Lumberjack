using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;
    [SerializeField] private float _attackRadius = 1f;
    [SerializeField] private float _damageAmount = 10f;
    [SerializeField] private float _attackDistance = 1f;

    public void Attack()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Calculate the position in front of the player for the sphere overlap
        Vector3 attackPosition = _playerModel.position + _playerModel.forward * _attackDistance;

        // Perform a sphere overlap to detect all colliders within the attack radius
        Collider[] colliders = Physics.OverlapSphere(attackPosition, _attackRadius);

        // Loop through all detected colliders and apply damage to the appropriate targets
        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to an object that can take damage
            EnemyHealth damageable = collider.GetComponent<EnemyHealth>();
            if (damageable != null)
            {
                // Apply damage to the damageable object
                damageable.TakeDamage(_damageAmount);
            }
        }
    }
}
