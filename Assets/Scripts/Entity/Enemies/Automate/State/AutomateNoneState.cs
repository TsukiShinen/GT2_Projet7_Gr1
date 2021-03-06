using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateNoneState : IState
{
    private AutomateController _automate;

    public AutomateNoneState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (!DayNightManager.Instance.IsDay)
            return _automate.WanderState;
        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _automate.Animator.Play("IdleSleep");
        _automate.Rigidbody.velocity = Vector3.zero;
    }

    public void Exit()
    {
        _automate.Animator.Play("Idle");
    }
}
