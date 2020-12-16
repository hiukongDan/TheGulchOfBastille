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
        if(enemy.refPlayer.position.x - enemy.aliveGO.transform.position.x > 0 != enemy.facingDirection > 0)
        {
            enemy.Flip();
        }

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
        if(Time.time > walkDurationTime + startTime || enemy.IsPlayerWithinMeleeAttackRange())
        {
            stateMachine.SwitchState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
