using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();
    private Transform _target;

    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _followRadius = 7f;
    [SerializeField] private float _speed = 10f;

    private void Start()
    {
        _agent.speed = _speed;

        InvokeRepeating("AttemptDetection", 0f, 1f);
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
        CancelInvoke("AttemptDetection");
        InvokeRepeating("FollowTarget", 0f, 1f);
    }

    private void FollowTarget()
    {
        if (_target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);

            if (distanceToTarget <= _followRadius)
            {
                _agent.SetDestination(_target.position);
                return;
            }
        }

        LostTarget();
    }


    private void LostTarget()
    {
        _target = null;
        CancelInvoke("FollowTarget");
        _agent.SetDestination(transform.position);
        InvokeRepeating("AttemptDetection", 0f, 1f);
    }
}
