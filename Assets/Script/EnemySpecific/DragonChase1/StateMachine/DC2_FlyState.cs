using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC2_FlyState : State
{
    public DC2_FlyState(FiniteStateMachine stateMachine, Entity entity, string animName)
    :base(stateMachine, entity, animName)
    {
        
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

    // Complete this state, usually used for setting control boolean
    public override void Complete()
    {
        base.Complete();
    }
    
}
