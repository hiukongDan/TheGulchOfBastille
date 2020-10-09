using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : State
{
    protected InAirStateData data;
    public InAirState(FiniteStateMachine stateMachine, Entity entity, string animName, InAirStateData stateData) : base(stateMachine, entity, animName)
    {
        this.data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
    }

    public override void Enter()
    {
        base.Enter();
        DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoChecks();
    }
}
