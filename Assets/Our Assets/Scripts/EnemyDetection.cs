using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _followRadius = 7f;
    private Transform _target;
    private EnemyMovement _movement => GetComponent<EnemyMovement>();
    private EnemyStateMachine _enemyStateMachine => GetComponent<EnemyStateMachine>();

    // Start is called before the first frame update
    void Start()
    {
        _enemyStateMachine.SetState(AttemptDetection, 1);
    }

    private void AttemptDetection()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, _detectionRadius);

        foreach (Collider target in targets)
        {
            if (target.GetComponent<CharacterController>() != null)
            {
                TargetDetected(target.transform);
                return;
            }
        }
    }

    private void TargetDetected(Transform target)
    {
        _target = target;
        _enemyStateMachine.SetState(FollowingTarget, 1);
    }

    private void FollowingTarget()
    {
        if (_target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);

            if (distanceToTarget <= _followRadius)
            {
                _movement.FollowTarget(_target);
                return;
            }
        }

        LostTarget();
    }

    private void LostTarget()
    {
        _target = null;
        _movement.StopInPlace();
        _enemyStateMachine.SetState(AttemptDetection, 1);
    }
}
