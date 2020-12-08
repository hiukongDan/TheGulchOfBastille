using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_DefenceState : DefenceState
{
    public GC1_DefenceState(FiniteStateMachine stateMachine, Entity entity, string animName, DefenceStateData stateData, State defaultNextState = null) : base(stateMachine, entity, animName, stateData, defaultNextState)
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
