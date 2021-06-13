﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_DiveState : State
{
    protected DragonCombat1 enemy;
    protected MeleeAttackStateData attackData;
    protected bool hasApplyDamage;
    public DC1_DiveState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy, MeleeAttackStateData attackData) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
        this.attackData = attackData;
    }
    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override void Complete()
    {
        base.Complete();
        stateMachine.SwitchState(enemy.landState);
    }

    public virtual void ApplyDamage(){
        CombatData combatData = attackData.GetCombatData();
        combatData.from = entity.aliveGO;
        combatData.position = entity.aliveGO.transform.position;

        Collider2D[] others = Physics2D.OverlapBoxAll(entity.damageBox.position, entity.DamageBoxSize, attackData.whatIsPlayer);
        foreach (Collider2D collider in others)
        {
            if (collider.gameObject.tag == "Player")
            {
                collider.gameObject.SendMessage("Damage", combatData);
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.dc1_ota.diveState = this;

        hasApplyDamage = false;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.dc1_ota.diveState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!hasApplyDamage && Gulch.Math.AlmostEqual(enemy.refPlayer.transform.position.y, enemy.aliveGO.transform.position.y, 0.8f)){
            hasApplyDamage = true;
            ApplyDamage();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer()
    {

    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
    }

}
