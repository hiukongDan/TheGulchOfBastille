using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected StunStateData data;

    protected float stunStartTime;
    protected bool isStunOver;
    public StunState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, StunStateData stateData) : base(stateMachine, entity, animBoolName)
    {
        data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        stunStartTime = Time.time;
        isStunOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time > stunStartTime + data.stunDuration && !isStunOver)
        {
            isStunOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
