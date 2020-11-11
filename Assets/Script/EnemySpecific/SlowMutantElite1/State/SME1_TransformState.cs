using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_TransformState : State
{
    protected SlowMutantElite1 enemy;
    public SME1_TransformState(FiniteStateMachine stateMachine, Entity entity, string animName, SlowMutantElite1 enemy) : base(stateMachine, entity, animName)
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
        enemy.SetIsDamageable(false);

        var ota = (SME1_ObjectToAlive)enemy.objectToAlive;
        ota.transformState = this;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetIsDamageable(true);

        var ota = (SME1_ObjectToAlive)enemy.objectToAlive;
        ota.transformState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void CompleteTransform()
    {
        stateMachine.SwitchState(enemy.stageTwoIdleState);
    }
}
