using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_WalkState : WalkState
{
    protected Enemy1 enemy;
    public E1_WalkState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, WalkStateData walkData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, walkData)
    {
        this.enemy = enemy;
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
        else if (isWallDetected || isEdgeDetected)
        {
            vectorWorkspace.x = 0;
            vectorWorkspace.y = entity.rb.velocity.y;
            entity.rb.velocity = vectorWorkspace;

            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.SwitchState(enemy.idleState);
        }
        else
        {
            vectorWorkspace.x = entity.facingDirection * data.walkSpeed;
            vectorWorkspace.y = enemy.rb.velocity.y;
            enemy.rb.velocity = vectorWorkspace;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoChecks();
    }
}
