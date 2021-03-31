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
        DetectPlayerInMaxAgro();
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

        if(isEdgeDetected || isWallDetected)
        {
            stateMachine.SwitchState(enemy.flipState);
        }
        else if(detectPlayerInMaxAgro && enemy.detectPlayerState.CanAction())
        {
            //enemy.detectPlayerState.SetPlayerDetectedTrans(detectPlayerTrans);
            stateMachine.SwitchState(enemy.detectPlayerState);
            //Debug.Log("Yes Yes Yes");
        }
        else if (walkDurationTime < 0)
        {
            enemy.flipState.SetPrevState(enemy.walkState);
            stateMachine.SwitchState(enemy.flipState);
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
