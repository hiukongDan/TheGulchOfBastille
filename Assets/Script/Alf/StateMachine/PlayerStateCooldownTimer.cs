using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCooldownTimer
{
    protected List<PlayerState> states;
    public PlayerStateCooldownTimer()
    {
        states = new List<PlayerState>();
    }

    public void AddStateTimer(PlayerState state)
    {
        if (!states.Contains(state))
            states.Add(state);
    }

    public void RemoveStateTimer(PlayerState state)
    {
        states.Remove(state);
    }

    public void ClearStateTimer()
    {
        states.Clear();
    }

    public virtual void UpdateTimer()
    {
        foreach (PlayerState state in states)
        {
            state.UpdateTimer();
        }
    }
}
