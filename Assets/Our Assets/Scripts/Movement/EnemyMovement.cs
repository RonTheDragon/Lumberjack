using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();

    private void Start()
    {
        _agent.speed = _moveSpeed;
    }

    public void FollowTarget(Transform target)
    {       
        _agent.SetDestination(target.position);
    }

    public void StopInPlace()
    {
        _agent.SetDestination(transform.position);
    }

    protected override void OnMovementSpeedChanged()
    {
        base.OnMovementSpeedChanged();
        _agent.speed = _moveSpeed;
    }
}
