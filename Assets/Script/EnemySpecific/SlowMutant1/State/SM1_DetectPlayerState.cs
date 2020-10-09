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
            if (playerWithinAgroMin)
            {
                // TODO:
                // if player attack => FLEE
                // else WILD MELEEATTACK
                
                stateMachine.SwitchState(enemy.meleeAttackState);
            }
            else if (playerWithinMeleeRange)
            {
                // MELEEATTACK
                enemy.Flip();
                stateMachine.SwitchState(enemy.meleeAttackState);
            }
            else
            {
                enemy.Flip();
                stateMachine.SwitchState(enemy.fleeState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
