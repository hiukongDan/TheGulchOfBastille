using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_FlipState : FlipState
{
    protected float timer;
    protected float minTimer = 0.5f;
    public GC1_FlipState(FiniteStateMachine stateMachine, Entity entity, string animName) : base(stateMachine, entity, animName)
    {
        timer = -1;
    }

    public override bool CanAction()
    {
        return timer < 0f;
    }
    public override void ResetTimer() => timer = minTimer;

    public override void UpdateTimer()
    {
        if(timer >= 0f)
        {
            timer -= Time.deltaTime;
        }
    }
}
