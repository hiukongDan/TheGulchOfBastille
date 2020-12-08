using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_EvadeState : EvadeState
{
    protected GoyeCombat1 enemy;
    public GC1_EvadeState(FiniteStateMachine stateMachine, Entity entity, string animName, EvadeStateData stateData, GoyeCombat1 enemy) : base(stateMachine, entity, animName, stateData)
    {
        this.enemy = enemy;
    }

    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override void CompleteEvade()
    {
        base.CompleteEvade();
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
