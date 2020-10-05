using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    protected Entity entity;
    protected State currentState;

    public FiniteStateMachine(Entity entity)
    {
        this.entity = entity;
    }

    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void SwitchState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    public void LogicUpdate()
    {
        currentState.LogicUpdate();
    }

    public void PhysicsUpdate()
    {
        currentState.PhysicsUpdate();
    }

}
