using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_HeideAttackState : MeleeAttackState
{
    protected SlowMutantElite1 enemy;
    protected int heideAttacktimesRemain;
    protected bool isHeideAttack;

    protected float cooldownTimer;

    public SME1_HeideAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitBoxPoint, SlowMutantElite1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitBoxPoint)
    {
        this.enemy = enemy;
        cooldownTimer = -1f;
    }

    public override void CompleteMeleeAttack()
    {
        base.CompleteMeleeAttack();

        if(!isHeideAttack)
        {
            DoChecks();

            if (!detectPlayerInMaxAgro)
            {
                enemy.flipState.SetPrevState(enemy.walkState);
                stateMachine.SwitchState(enemy.flipState);
            }
            else
            {
                stateMachine.SwitchState(enemy.walkState);
            }

            ResetTimer();
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
        heideAttacktimesRemain = data.heideAttackTimes - 1;
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

        if(!isAttacking && heideAttacktimesRemain > 0)
        {
            heideAttacktimesRemain--;
            isAttacking = true;
        }
        else if(!isAttacking && heideAttacktimesRemain <= 0)
        {
            isHeideAttack = false;
            enemy.anim.SetBool("heideAttack", isHeideAttack);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer() => cooldownTimer = data.cooldownTimer;

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public override bool CanAction() => cooldownTimer < 0;
}
