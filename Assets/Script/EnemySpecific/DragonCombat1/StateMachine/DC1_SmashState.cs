using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_SmashState : State
{
    protected DragonCombat1 enemy;
    protected MeleeAttackStateData attackData;
    protected GameObject smashDust;
    //private bool hasApplyDamage;
    public DC1_SmashState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy, MeleeAttackStateData attackData, GameObject smashDust) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
        this.attackData = attackData;
        this.smashDust = smashDust;
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

    public void ReleaseSmashDust(){
        if(this.smashDust != null){
            GameObject.Instantiate(this.smashDust, enemy.aliveGO.transform.position, enemy.aliveGO.transform.rotation, enemy.aliveGO.transform);
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
