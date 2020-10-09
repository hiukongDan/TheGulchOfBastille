using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM1_TakeDamageState : TakeDamageState
{
    protected SlowMutant1 enemy;
    public SM1_TakeDamageState(FiniteStateMachine stateMachine, Entity entity, string animName, TakeDamageStateData stateData, SlowMutant1 enemy) : base(stateMachine, entity, animName, stateData)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isTakeDamage)
        {
            entity.Flip();
            stateMachine.SwitchState(enemy.fleeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
