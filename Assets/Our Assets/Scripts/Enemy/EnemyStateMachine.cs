using System;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private float _currentCooldown;
    private float _cooldownLength;
    private Action _action;

    // Update is called once per frame
    void Update()
    {
        StateLoop();
    }

    void StateLoop()
    {
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
        else
        {
            _currentCooldown = _cooldownLength;
            _action?.Invoke();
        }
    }

    public void SetState(Action state, float cooldown)
    {
        _cooldownLength = cooldown;
        _action= state;
        _currentCooldown = 0;
    }
}
