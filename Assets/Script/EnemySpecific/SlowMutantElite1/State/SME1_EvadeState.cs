using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_EvadeState : EvadeState
{
    private SlowMutantElite1 enemy;

    protected float cooldownTimer;

    public SME1_EvadeState(FiniteStateMachine stateMachine, Entity entity, string animName, EvadeStateData stateData, SlowMutantElite1 enemy) : base(stateMachine, entity, animName, stateData)
    {
        this.enemy = enemy;
        cooldownTimer = -1f;
    }

    public override void CompleteEvade()
    {
        base.CompleteEvade();

        ResetTimer();

        stateMachine.SwitchState(enemy.walkState);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        DetectPlayerInMeleeAttackRange();
        DetectPlayerInMaxAgro();
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

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public override bool CanAction() => cooldownTimer < 0;

    public override void ResetTimer() => cooldownTimer = data.cooldownTimer;

}
