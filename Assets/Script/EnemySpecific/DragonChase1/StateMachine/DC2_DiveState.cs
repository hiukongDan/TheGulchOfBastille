using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC2_DiveState : DC1_DiveState
{
    protected DragonChase2 dragon;
    public DC2_DiveState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonChase2 dragon, MeleeAttackStateData attackData)
    : base(stateMachine, entity, animName, null, attackData)
    {
        this.dragon = dragon;
    }

    public override void Enter()
    {
        if(entity.anim.gameObject.activeInHierarchy){
            entity.anim.Play(animName);
            startTime = Time.time;
        }

        dragon.dc2_ota.diveState = this;
        hasApplyDamage = false;
    }

    public override void Exit()
    {
        dragon.dc2_ota.diveState = null;
    }

    public override void Complete()
    {
        stateMachine.SwitchState(dragon.flyIdleState);
    }

    public override void LogicUpdate()
    {
        if(!hasApplyDamage && Gulch.Math.AlmostEqual(dragon.refPlayer.transform.position.y, dragon.aliveGO.transform.position.y, 0.8f)){
            hasApplyDamage = true;
            ApplyDamage();
        }
    }

    public override void ApplyDamage(){
        CombatData combatData = attackData.GetCombatData();
        combatData.from = dragon.aliveGO;
        combatData.position = dragon.aliveGO.transform.position;

        Collider2D[] others = Physics2D.OverlapBoxAll(dragon.damageBox.position, dragon.DamageBoxSize, attackData.whatIsPlayer);
        foreach (Collider2D collider in others)
        {
            if (collider.gameObject.tag == "Player")
            {
                collider.gameObject.SendMessage("Damage", combatData);
            }
        }
    }

}
