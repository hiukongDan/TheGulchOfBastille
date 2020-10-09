using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageState : State
{
    protected TakeDamageStateData data;
    protected bool isTakeDamage;
    public TakeDamageState(FiniteStateMachine stateMachine, Entity entity, string animName, TakeDamageStateData stateData) : base(stateMachine, entity, animName)
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

        isTakeDamage = true;
        entity.objectToAlive.takeDamageState = this;
    }

    public override void Exit()
    {
        base.Exit();
        entity.objectToAlive.takeDamageState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void CompleteTakeDamage()
    {
        isTakeDamage = false;
    }
}
