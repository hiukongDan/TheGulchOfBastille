using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM1_StunState : StunState
{
    protected SlowMutant1 enemy;
    public SM1_StunState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, StunStateData stateData, SlowMutant1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CompleteStun()
    {
        base.CompleteStun();

        // SHOULD BE LOOK FOR PLAYER STATE
        enemy.Flip();
        stateMachine.SwitchState(enemy.meleeAttackState);
    }
}
