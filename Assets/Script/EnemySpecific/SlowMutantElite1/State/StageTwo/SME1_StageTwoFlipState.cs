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
        enemy.snakeHeadsParent.gameObject.SetActive(false);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.snakeHeadsParent.gameObject.SetActive(true);
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
