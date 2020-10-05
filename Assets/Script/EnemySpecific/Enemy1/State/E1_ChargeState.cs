using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    protected Enemy1 enemy;
    public E1_ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, ChargeStateData stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
        enemy.rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isWallDetected || isEdgeDetected)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.SwitchState(enemy.idleState);
        }
        else if(detectPlayerInMeleeRange)
        {
            stateMachine.SwitchState(enemy.meleeAttackState);
        }
        else if(isChargeTimeOver && !detectPlayerInMaxAgro)
        {
            stateMachine.SwitchState(enemy.lookForPlayerState);
        }
        else if(!isChargeTimeOver)
        {
            entity.rb.velocity = new Vector2(entity.facingDirection * data.chargeSpeed, entity.rb.velocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
