using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    public PlayerStateCooldownTimer stateCooldownTimer { get; private set; }

    public PlayerStateMachine()
    {
        
    }

    public void SetStateCooldownTimer(PlayerStateCooldownTimer timer)
    {
        stateCooldownTimer = timer;
    }

    public void InitializeState(PlayerState startingState)
    {
        currentState = startingState;
        startingState?.Enter();
    }

    public void LogicUpdate()
    {
        currentState.LogicUpdate();

        stateCooldownTimer?.UpdateTimer();
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

        //Debug.Log(newState.ToString());
    }
}
