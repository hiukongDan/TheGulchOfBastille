using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_StageTwoIdleState : IdleState
{
    public SlowMutantElite1 enemy;
    public SME1_StageTwoIdleState(FiniteStateMachine stateMachine, Entity entity, string animName, IdleStateData stateData, SlowMutantElite1 enemy) : base(stateMachine, entity, animName, stateData)
    {
        this.enemy = enemy;
        SetFlipAfterIdle(false);
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

        if (startTime + idleDurationTime > Time.time && enemy.detectPlayerState.CanAction())
        {
            stateMachine.SwitchState(enemy.detectPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
