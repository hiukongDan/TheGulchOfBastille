using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_ChargeState : MeleeAttackState
{
    protected SlowMutantElite1 enemy;
    protected ChargeStateData chargeStateData;

    protected float cooldownTimer;

    public SME1_ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, ChargeStateData chargeStateData, Transform hitBoxPoint, SlowMutantElite1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitBoxPoint)
    {
        this.enemy = enemy;
        this.chargeStateData = chargeStateData;

        cooldownTimer = -1;
    }

    public override void CompleteMeleeAttack()
    {
        DoChecks();

        ResetTimer();

        stateMachine.SwitchState(enemy.walkState);

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void DoMeleeAttack()
    {
        //base.DoMeleeAttack();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitBoxPoint.position, enemy.DamageBoxSize, data.whatIsPlayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                combatData.position = entity.aliveGO.transform.position;
                collider.gameObject.SendMessage("Damage", combatData);
            }
        }
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public override void ResetTimer() => cooldownTimer = chargeStateData.chargeCoolDownTime;

    public override bool CanAction() => cooldownTimer < 0;

}
