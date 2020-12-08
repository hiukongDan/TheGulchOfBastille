using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_IdleState : IdleState
{
    protected GoyeCombat1 enemy;
    public GC1_IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, IdleStateData idleData, GoyeCombat1 enemy) : base(stateMachine, entity, animBoolName, idleData)
    {
        this.enemy = enemy;
    }

    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
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
        base.ResetTimer();
    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
    }
}
