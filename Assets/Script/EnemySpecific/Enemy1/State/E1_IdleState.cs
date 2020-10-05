using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{
    private Enemy1 enemy;
    public E1_IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, IdleStateData idleData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, idleData)
    {
        this.enemy = enemy;
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
        if (detectPlayerInMinAgro)
        {
            enemy.detectPlayerState.SetPlayerDetectedTrans(detectPlayerTrans);
            stateMachine.SwitchState(enemy.detectPlayerState);
        }
        else if (Time.time > stateStartTime + idleDurationTime)
        {
            stateMachine.SwitchState(enemy.walkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoChecks();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
