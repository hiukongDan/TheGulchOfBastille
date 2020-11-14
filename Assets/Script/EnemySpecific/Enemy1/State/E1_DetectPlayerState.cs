using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class E1_DetectPlayerState : DetectPlayerState
{
    protected Enemy1 enemy;
    public E1_DetectPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DetectPlayerStateData stateData, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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

        DoChecks();

        if (detectPlayerInMeleeRange)
        {
            stateMachine.SwitchState(enemy.meleeAttackState);
        }
        else
        {
            stateMachine.SwitchState(enemy.chargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
