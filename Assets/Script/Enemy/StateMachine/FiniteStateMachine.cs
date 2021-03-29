using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    protected Entity entity;
    public State currentState { get; private set; }
    public StateCooldownTimer stateCooldownTimer { get; private set; }

    public FiniteStateMachine(Entity entity)
    {
        this.entity = entity;
    }

    public void SetStateCooldownTimer(StateCooldownTimer stateCooldownTimer)
    {
        this.stateCooldownTimer = stateCooldownTimer;
    }

    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        newState.Enter();
    }

    public void LogicUpdate()
    {
        currentState?.LogicUpdate();

        stateCooldownTimer?.UpdateTimer();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }

}
