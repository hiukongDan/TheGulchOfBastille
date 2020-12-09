using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_DefenceState : DefenceState
{
    protected GoyeCombat1 enemy;
    protected float cooldownTimer;
    public GC1_DefenceState(FiniteStateMachine stateMachine, Entity entity, string animName, DefenceStateData stateData, GoyeCombat1 enemy, State defaultNextState = null) : base(stateMachine, entity, animName, stateData, defaultNextState)
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

    public override void ResetTimer() => cooldownTimer = data.defenceCooldown;

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
