using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_SmashState : State
{
    protected DragonCombat1 enemy;
    protected MeleeAttackStateData attackData;
    //private bool hasApplyDamage;
    public DC1_SmashState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy, MeleeAttackStateData attackData) : base(stateMachine, entity, animName)
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
        stateMachine.SwitchState(enemy.idleState);
    }

    public void ApplyDamage(){
        CombatData combatData = attackData.GetCombatData();
        combatData.from = enemy.aliveGO;
        combatData.position = enemy.aliveGO.transform.position;

        Collider2D[] others = Physics2D.OverlapBoxAll(enemy.damageBox.position, enemy.DamageBoxSize, attackData.whatIsPlayer);
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
        enemy.dc1_ota.smashState = this;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.dc1_ota.smashState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // if(!hasApplyDamage && Gulch.Math.AlmostEqual(enemy.refPlayer.transform.position.y, enemy.aliveGO.transform.position.y, 0.3f)){
        //     hasApplyDamage = true;
        //     ApplyDamage();
        // }
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
