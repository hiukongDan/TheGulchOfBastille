using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_StageTwoFlipState : SME1_FlipState
{
    public SME1_StageTwoFlipState(FiniteStateMachine stateMachine, Entity entity, string animName, State defaultPrevState, SlowMutantElite1 enemy) : base(stateMachine, entity, animName, defaultPrevState, enemy)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        foreach(SME1_SnakeHead head in enemy.SnakeHeads)
        {
            head.Hide();
        }
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
}
