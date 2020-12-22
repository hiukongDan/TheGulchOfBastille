using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryState : State
{
    protected ParryStateData data;
    protected CombatData cbData;
    public ParryState(FiniteStateMachine stateMachine, Entity entity, string animName, ParryStateData data) : base(stateMachine, entity, animName)
    {
        this.data = data;
    }

    protected virtual void InitCombatData()
    {
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

        InitCombatData();
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

    public virtual void DoParry()
    {

    }

}
