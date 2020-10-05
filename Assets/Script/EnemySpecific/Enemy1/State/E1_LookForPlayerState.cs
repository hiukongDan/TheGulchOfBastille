using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_LookForPlayerState : LookForPlayerState
{
    protected Enemy1 enemy;
    public E1_LookForPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, LookForPlayerStateData stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
        if (detectPlayerInMinAgro)
        {
            enemy.detectPlayerState.SetPlayerDetectedTrans(detectPlayerTrans);
            stateMachine.SwitchState(enemy.detectPlayerState);
        }
        else if (waitTimeOver && turnTimesLeft >= 0)
        {
            entity.Flip();
            waitTimeOver = false;
        }
        else
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.SwitchState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
