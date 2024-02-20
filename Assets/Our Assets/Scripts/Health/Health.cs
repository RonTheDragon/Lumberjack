using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    [ReadOnly][SerializeField] protected float _health;
    [ReadOnly][SerializeField] protected bool _isDead;


    // Start is called before the first frame update
    void Start()
    {
        HealToMax();
    }

    [ContextMenu("Heal To Max")]
    public void HealToMax()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) 
            return;

        _health -= damage;

        DieIfNoHealth();
    }

    private void DieIfNoHealth()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    [ContextMenu("Die")]
    public virtual void Die()
    {
        _health = 0;
        _isDead = true;
    }
}
