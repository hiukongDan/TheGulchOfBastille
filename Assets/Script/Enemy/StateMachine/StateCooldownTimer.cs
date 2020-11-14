using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCooldownTimer
{
    protected List<State> states;

    public StateCooldownTimer()
    {
        states = new List<State>();
    }

    public void AddStateTimer(State state)
    {
        if(!states.Contains(state))
            states.Add(state);
    }

    public void RemoveStateTimer(State state)
    {
        states.Remove(state);
    }

    public void ClearStateTimer()
    {
        states.Clear();
    }

    public virtual void UpdateTimer()
    {
        foreach(State state in states)
        {
            state.UpdateTimer();
        }
    }
}
