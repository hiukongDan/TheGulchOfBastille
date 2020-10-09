using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    protected FleeStateData data;
    public FleeState(FiniteStateMachine stateMachine, Entity entity, string animName, FleeStateData stateData) : base(stateMachine, entity, animName)
    {
        this.data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectPlayerInMaxAgro();
        DetectPlayerInMinAgro();
        DetectPlayerInMeleeAttackRange();
    }

    public override void Enter()
    {
        base.Enter();
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

        DetectSurroundings();
    }
}
