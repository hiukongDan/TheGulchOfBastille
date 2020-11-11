using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_EvadeState : EvadeState
{
    private SlowMutantElite1 enemy;
    public SME1_EvadeState(FiniteStateMachine stateMachine, Entity entity, string animName, SlowMutantElite1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
    }

    public override void CompleteEvade()
    {
        base.CompleteEvade();

        stateMachine.SwitchState(enemy.heideAttackState);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
