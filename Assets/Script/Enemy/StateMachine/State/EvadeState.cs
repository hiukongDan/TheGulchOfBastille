using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : State
{
    protected bool isEvade;
    protected EvadeStateData data;
    public EvadeState(FiniteStateMachine stateMachine, Entity entity, string animName, EvadeStateData stateData) : base(stateMachine, entity, animName)
    {
        this.data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        entity.objectToAlive.evadeState = this;
        isEvade = true;
    }

    public override void Exit()
    {
        base.Exit();
        entity.objectToAlive.evadeState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void CompleteEvade()
    {
        isEvade = false;
    }
}
