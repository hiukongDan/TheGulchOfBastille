using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_StageTwoIdleState : State
{
    public SlowMutantElite1 enemy;
    public SME1_StageTwoIdleState(FiniteStateMachine stateMachine, Entity entity, string animName, SlowMutantElite1 enemy) : base(stateMachine, entity, animName)
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
