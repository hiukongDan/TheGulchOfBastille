using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_HeideAttackState : MeleeAttackState
{
    protected SlowMutantElite1 enemy;
    public int heideAttacktimes;
    private bool isHeideAttack;
    public SME1_HeideAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitBoxPoint, SlowMutantElite1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitBoxPoint)
    {
        this.enemy = enemy;
    }

    public override void CompleteMeleeAttack()
    {
        base.CompleteMeleeAttack();
        
        if(!isHeideAttack)
        {
            enemy.flipState.SetPrevState(enemy.walkState);
            stateMachine.SwitchState(enemy.flipState);
        }
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
        isHeideAttack = true;
        enemy.anim.SetBool("heideAttack", isHeideAttack);
        enemy.anim.Play(animName);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isAttacking && heideAttacktimes > 0)
        {
            heideAttacktimes--;
            isAttacking = true;
        }
        else if(!isAttacking && heideAttacktimes <= 0)
        {
            isHeideAttack = false;
            enemy.anim.SetBool("heideAttack", isHeideAttack);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
