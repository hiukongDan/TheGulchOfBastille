using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_WalkState : WalkState
{
    protected SlowMutantElite1 enemy;
    public SME1_WalkState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, WalkStateData walkData, SlowMutantElite1 enemy) : base(stateMachine, entity, animBoolName, walkData)
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

        // if this.walkDuration has been exceeded to a threshold, switch to turn state / or find edge
        // else if find player, change to detect player state
        // else do nothing

        if(walkDurationTime < 0)
        {
            stateMachine.SwitchState(enemy.heideAttackState);
            //stateMachine.SwitchState(enemy.flipState);
        }
        else
        {
            walkDurationTime -= Time.deltaTime;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
