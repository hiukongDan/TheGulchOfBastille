using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_ChargeState : ChargeState
{
    protected GoyeCombat1 enemy;
    public GC1_ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, ChargeStateData stateData, GoyeCombat1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
