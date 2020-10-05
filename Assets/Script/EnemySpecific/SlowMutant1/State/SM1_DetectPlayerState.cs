using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM1_DetectPlayerState : DetectPlayerState
{
    protected SlowMutant1 enemy;
    
    public SM1_DetectPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DetectPlayerStateData stateData, SlowMutant1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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

        if (Time.time > startTime + data.detectStayTime)
        {
            if (playerWithinMeleeRange)
            {
                // TODO:
                // if player attack => FLEE
                // else WILD MELEEATTACK

                // flee, for now
                entity.Flip();
                stateMachine.SwitchState(enemy.fleeState);
            }
            else if (playerWithinAgroMin)
            {
                // MELEEATTACK
                stateMachine.SwitchState(enemy.meleeAttackState);
            }
            else
            {
                stateMachine.SwitchState(enemy.idleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
