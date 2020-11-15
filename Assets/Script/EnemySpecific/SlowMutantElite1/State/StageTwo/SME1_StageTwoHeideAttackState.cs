using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_StageTwoHeideAttackState : MeleeAttackState
{
    protected SlowMutantElite1 enemy;
    protected int heideAttacktimesRemain;
    protected bool isHeideAttack;

    protected float cooldownTimer;

    private string animLoopName;

    public SME1_StageTwoHeideAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitBoxPoint, SlowMutantElite1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitBoxPoint)
    {
        this.enemy = enemy;
        cooldownTimer = -1f;
        animLoopName = animBoolName + "_loop";
    }

    public override void CompleteMeleeAttack()
    {
        base.CompleteMeleeAttack();

        if (!isHeideAttack)
        {
            stateMachine.SwitchState(enemy.stageTwoIdleState);

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
        foreach (SME1_SnakeHead head in enemy.SnakeHeads)
        {
            head.Hide();
        }

        base.Enter();
        heideAttacktimesRemain = data.heideAttackTimes - 1;
        isHeideAttack = true;
        enemy.anim.SetBool(animName, isHeideAttack);
        enemy.anim.Play(animName);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAttacking && heideAttacktimesRemain > 0)
        {
            heideAttacktimesRemain--;
            isAttacking = true;
            enemy.anim.Play(animLoopName);
        }
        else if (!isAttacking && heideAttacktimesRemain <= 0 && isHeideAttack)
        {
            isHeideAttack = false;
            enemy.anim.SetBool(animName, isHeideAttack);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer() => cooldownTimer = data.cooldownTimer;

    public override void UpdateTimer()
    {
        if (cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public override bool CanAction() => cooldownTimer < 0;
}
