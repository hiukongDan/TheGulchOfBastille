using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GC1_ParryState : ParryState
{
    protected GoyeCombat1 enemy;
    //protected float cooldownTimer;
    
    public GC1_ParryState(FiniteStateMachine stateMachine, Entity entity, string animName, ParryStateData stateData, GoyeCombat1 enemy) : base(stateMachine, entity, animName, stateData)
    {
        this.enemy = enemy;
        //cooldownTimer = -1f;
    }

    protected override void InitCombatData()
    {
        cbData.damage = data.damage;
        cbData.facingDirection = enemy.facingDirection;
        cbData.from = enemy.gameObject;
        cbData.isParryDamage = true;
        cbData.knockbackDir = enemy.meleeAttackStateData.knockbackDir;
        cbData.knockbackImpulse = enemy.meleeAttackStateData.knockbackImpulse;
        cbData.position = enemy.aliveGO.transform.position;
        cbData.stunDamage = 0f;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.gc1_ota.parryState = this;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.gc1_ota.parryState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoParry()
    {
        enemy.refPlayer.SendMessage("Damage", cbData);
    }

    public override void Complete()
    {
        stateMachine.SwitchState(enemy.combatIdleState);
    }


    //public override bool CanAction() => cooldownTimer < 0f;

    //public override void ResetTimer() => cooldownTimer = data.cooldownTimer;

    /*
    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    */
}
