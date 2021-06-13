using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC2_SleepState : State
{
    protected DragonChase2 enemy;
    public DC2_SleepState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonChase2 enemy)
    :base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.dc2_ota.sleepState = this;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.dc2_ota.sleepState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
        
    }

}
