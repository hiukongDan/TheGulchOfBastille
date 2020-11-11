using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_StoneState : State
{
    protected SlowMutantElite1 enemy;
    private bool isRecover;
    public SME1_StoneState(FiniteStateMachine stateMachine, Entity entity, string animName, SlowMutantElite1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isRecover = false;

        enemy.SetIsDamageable(false);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.SetIsDamageable(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isRecover)
        {
            stateMachine.SwitchState(enemy.recoverState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void TriggerRecover() => isRecover = true;
}
