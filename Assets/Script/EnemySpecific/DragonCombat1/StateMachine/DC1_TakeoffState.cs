using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_TakeoffState : State
{
    protected DragonCombat1 enemy;
    public DC1_TakeoffState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
    }
    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override void Complete()
    {
        stateMachine.SwitchState(enemy.flyIdleState);
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Enter()
    {
        base.Enter();
        enemy.dc1_ota.takeoffState = this;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.dc1_ota.takeoffState = null;
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
