using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SM1_FleeState : FleeState
{
    protected SlowMutant1 enemy;
    public SM1_FleeState(FiniteStateMachine stateMachine, Entity entity, string animName, FleeStateData stateData, SlowMutant1 enemy) : base(stateMachine, entity, animName, stateData)
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

        // a mess here, clean up in future
        if (Time.time > startTime + data.fleeTime || isEdgeDetected || isWallDetected)
        {
            enemy.Flip();
            stateMachine.SwitchState(enemy.walkState);
        }
        else if (detectPlayerInMinAgro)
        {
            stateMachine.SwitchState(enemy.meleeAttackState);
        }
        else
        {
            enemy.rb.velocity = new Vector2(entity.facingDirection * data.fleeSpeed, enemy.rb.velocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
