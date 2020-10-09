using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected DeadStateData data;
    public DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DeadStateData stateData) : base(stateMachine, entity, animBoolName)
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
        entity.rb.velocity = new Vector2(0f, entity.rb.velocity.y);
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
    }
}
