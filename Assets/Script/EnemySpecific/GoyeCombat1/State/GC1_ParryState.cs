using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GC1_ParryState : ParryState
{
    protected GoyeCombat1 enemy;
    protected float cooldownTimer;
    public GC1_ParryState(FiniteStateMachine stateMachine, Entity entity, string animName, ParryStateData stateData, GoyeCombat1 enemy) : base(stateMachine, entity, animName, stateData)
    {
        this.enemy = enemy;
        cooldownTimer = -1f;
    }

    public override bool CanAction() => cooldownTimer < 0f;

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer() => cooldownTimer = data.cooldownTimer;

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
