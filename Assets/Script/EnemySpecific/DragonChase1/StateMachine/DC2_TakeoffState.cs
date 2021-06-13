using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC2_TakeoffState : DC1_TakeoffState
{
    protected DragonChase2 dragon;
    public DC2_TakeoffState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonChase2 enemy)
    :base(stateMachine, entity, animName, null)
    {
        this.dragon = enemy;
    }

    public override void Enter()
    {
        if(entity.anim.gameObject.activeInHierarchy){
            entity.anim.Play(animName);
            startTime = Time.time;
        }

        dragon.dc2_ota.takeoffState = this;
    }

    public override void Exit()
    {
        dragon.dc2_ota.takeoffState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // enemy.refPlayer.transform.position
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
    }

    public override bool CanAction()
    {
        return true;
    }

    public override void ResetTimer()
    {
        base.ResetTimer();
    }

    public override void Complete()
    {
        stateMachine.SwitchState(dragon.flyIdleState);
    }
    
}
