using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM1_HeideAttackState : MeleeAttackState
{
    protected SlowMutant1 enemy;
    public int heideAttacktimes;
    public SM1_HeideAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitBoxPoint, SlowMutant1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitBoxPoint)
    {
        this.enemy = enemy;
    }

    public override void CompleteMeleeAttack()
    {
        base.CompleteMeleeAttack();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void DoMeleeAttack()
    {
        base.DoMeleeAttack();
    }

    public override void Enter()
    {
        base.Enter();
        heideAttacktimes = data.heideAttackTimes;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAttacking && heideAttacktimes > 0)
        {
            heideAttacktimes--;
            isAttacking = true;
            enemy.Flip();
            enemy.anim.Play(animName);
        }
        else if(!isAttacking && heideAttacktimes <= 0)
        {
            stateMachine.SwitchState(enemy.fleeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
