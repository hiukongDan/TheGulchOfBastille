using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_DefenceState : DefenceState
{
    protected GoyeCombat1 enemy;
    protected float cooldownTimer;

    protected bool isDefenceForward, isDefence;
    public bool isCounterAttack { get; protected set; }

    public GC1_DefenceState(FiniteStateMachine stateMachine, Entity entity, string animName, DefenceStateData stateData, GoyeCombat1 enemy, State defaultNextState = null) : base(stateMachine, entity, animName, stateData, defaultNextState)
    {
        this.enemy = enemy;
        cooldownTimer = -1f;
        isCounterAttack = false;
    }

    public override void Complete()
    {
        stateMachine.SwitchState(enemy.combatIdleState);
    }

    public void ActiveCounterAttack()
    {
        isCounterAttack = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        enemy.gc1_ota.defenceState = this;

        enemy.anim.SetBool("isDefenceForward", isDefenceForward);
        enemy.anim.SetBool("isDefence", true);
        isCounterAttack = false;

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        ResetTimer();
        isCounterAttack = false;

        enemy.gc1_ota.defenceState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(defenceTimer < 0)
        {
            enemy.anim.SetBool("isDefence", false);
            isCounterAttack = false;
        }
    }

    public void CounterAttack()
    {
        stateMachine.SwitchState(enemy.parryState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override bool CanAction() => cooldownTimer < 0f;
    public override void ResetTimer() => cooldownTimer = data.defenceCooldown;

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void SetIsDefenceForward(bool isDefenceForward) => this.isDefenceForward = isDefenceForward;
}
