using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private PlayerState currentState;

    public PlayerStateMachine()
    {
        
    }

    public void InitializeState(PlayerState startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void LogicUpdate()
    {
        currentState.LogicUpdate();
    }

    public void PhysicsUpdate()
    {
        currentState.PhysicsUpdate();
    }

    public void SwitchState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }
}
