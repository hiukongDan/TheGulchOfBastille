using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_RunState : WalkState
{
    protected GoyeCombat1 enemy;
    public GC1_RunState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, WalkStateData walkData, GoyeCombat1 enemy) : base(stateMachine, entity, animBoolName, walkData)
    {
        this.enemy = enemy;
    }

    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        enemy.FaceToPlayer();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // run near player or wait till time up

        enemy.FaceToPlayer();

        /* if (enemy.IsPlayerWithinMeleeAttackRange() && enemy.meleeAttackState.CanAction())
         {
             stateMachine.SwitchState(enemy.meleeAttackState);
         }*/
        if (enemy.detectPlayerState.CanAction())
        {
            stateMachine.SwitchState(enemy.detectPlayerState);
        }
        else if(Time.time > walkDurationTime + startTime)
        {
            stateMachine.SwitchState(enemy.combatIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
