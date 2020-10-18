using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM1_InAirState : InAirState
{
    protected SlowMutant1 enemy;

    public SM1_InAirState(FiniteStateMachine stateMachine, Entity entity, string animName, InAirStateData stateData, SlowMutant1 enemy) : base(stateMachine, entity, animName, stateData)
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

        if((isGroundDetected || isPlatformDetected) && enemy.rb.velocity.y < 0.01f)
        {
            stateMachine.SwitchState(enemy.meleeAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
