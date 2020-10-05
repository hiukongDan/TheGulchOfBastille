using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : State
{
    protected MeleeAttackStateData data;

    protected bool detectTarget;
    protected bool isAttacking;

    protected Transform hitBoxPoint;

    protected CombatData combatData;
    public MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitBoxPoint):base(stateMachine, entity, animBoolName)
    {
        data = stateData;
        this.hitBoxPoint = hitBoxPoint;
    }
    public override void DoChecks()
    {
        base.DoChecks();
        DetectPlayerInMaxAgro();
        detectTarget = entity.DetectPlayer();
    }

    public override void Enter()
    {
        base.Enter();
        entity.objectToAlive.meleeAttackState = this;
        DoChecks();
        isAttacking = true;
        entity.rb.velocity = Vector2.zero;

        combatData.damage = data.damage;
        combatData.stunDamage = data.stunDamage;
        combatData.knockbackDir = data.knockbackDir;
        combatData.knockbackImpulse = data.knockbackImpulse;
    }

    public override void Exit()
    {
        base.Exit();
        entity.objectToAlive.meleeAttackState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoChecks();
    }

    public virtual void DoMeleeAttack()
    {
        Collider2D detectedTarget = Physics2D.OverlapCircle(hitBoxPoint.position, data.attackRadius, data.whatIsPlayer);
        if (detectedTarget != null)
        {
            combatData.position = entity.aliveGO.transform.position;
            detectedTarget.SendMessage("Damage", combatData);
        }
    }

    public virtual void CompleteMeleeAttack()
    {
        isAttacking = false;
    } 

}
