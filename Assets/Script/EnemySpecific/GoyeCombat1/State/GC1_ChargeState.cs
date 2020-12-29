using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_ChargeState : MeleeAttackState
{
    protected GoyeCombat1 enemy;
    protected float cooldownTimer;
    public GC1_ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitbox, GoyeCombat1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitbox)
    {
        this.enemy = enemy;
        cooldownTimer = -1f;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.FaceToPlayer();

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

    public override void DoMeleeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitBoxPoint.position, enemy.DamageBoxSize, enemy.entityData.whatIsPlayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                combatData.position = entity.aliveGO.transform.position;
                combatData.facingDirection = enemy.facingDirection;
                collider.gameObject.SendMessage("Damage", combatData);
            }
        }
    }

    public override void CompleteMeleeAttack()
    {
        stateMachine.SwitchState(enemy.combatIdleState);
    }

    public override bool CanAction() => cooldownTimer < 0;

    public override void ResetTimer() => cooldownTimer = data.cooldownTimer;

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
