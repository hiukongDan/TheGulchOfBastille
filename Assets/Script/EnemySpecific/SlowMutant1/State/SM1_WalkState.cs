using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SM1_WalkState : WalkState
{
    protected SlowMutant1 enemy;

    public SM1_WalkState(FiniteStateMachine stateMachine, Entity entity, string animName, WalkStateData stateData, SlowMutant1 enemy) : base(stateMachine, entity, animName, stateData)
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
        if(isEdgeDetected || isWallDetected)
        {
            enemy.Flip();
        }

        //vectorWorkspace.Set(data.walkSpeed * enemy.facingDirection, 0);
        //enemy.rb.velocity = vectorWorkspace;

        if (detectPlayerInMinAgro)
        {
            enemy.detectPlayerState.SetPlayerDetectedTrans(detectPlayerTrans);
            stateMachine.SwitchState(enemy.detectPlayerState);
        }

        if(Time.time > startTime + walkDurationTime)
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
