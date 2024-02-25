using UnityEngine;

public class ShotgunProjectile : MonoBehaviour
{
    private Vector3 _originalSize;
    private float _damage;
    private float _speed;
    private float _growSpeed;
    private float _timeLeft;

    public void Initialize(float damage, float speed, float growSpeed, float timeLeft)
    {
        transform.localScale = _originalSize;
        _damage = damage;
        _speed = speed;
        _growSpeed = growSpeed;
        _timeLeft = timeLeft;
    }

    private void Awake()
    {
        _originalSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        transform.localScale += new Vector3(_growSpeed * Time.deltaTime, 0f, 0f);
        if (_timeLeft > 0) 
        {
            _timeLeft -= Time.deltaTime; 
        }
        else 
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
        }
    }
}
