using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_MeleeAttackState : MeleeAttackState
{
    protected GoyeCombat1 enemy;
    protected float cooldownTimer;
    public GC1_MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitBoxPoint, GoyeCombat1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitBoxPoint)
    {
        this.enemy = enemy;
        cooldownTimer = -1f;
    }

    public override bool CanAction() => cooldownTimer < 0f;

    public override void CompleteMeleeAttack()
    {
        //base.CompleteMeleeAttack();
        stateMachine.SwitchState(enemy.combatIdleState);
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

        combatData.isParryDamage = false;
    }

    public override void Exit()
    {
        base.Exit();
        ResetTimer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer(){
        cooldownTimer = data.cooldownTimer;
    }

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
