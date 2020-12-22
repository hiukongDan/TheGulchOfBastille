using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceState : State
{
    protected DefenceStateData data;

    protected float defenceTimer;

    public DefenceState(FiniteStateMachine stateMachine, Entity entity, string animName, DefenceStateData data, State defaultNextState = null) : base(stateMachine, entity, animName, defaultNextState)
    {
        this.data = data;
    }

    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        defenceTimer = data.defenceDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(defenceTimer >= 0f)
        {
            defenceTimer -= Time.deltaTime;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer()
    {
        base.ResetTimer();
    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
    }
}
