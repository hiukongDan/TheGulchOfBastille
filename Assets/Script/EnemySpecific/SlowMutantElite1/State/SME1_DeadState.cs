using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_DeadState : DeadState
{
    protected SlowMutantElite1 enemy;
    public SME1_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DeadStateData stateData, SlowMutantElite1 enemy) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
    }
}
