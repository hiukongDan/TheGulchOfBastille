using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_FlipState : FlipState
{
    protected SlowMutantElite1 enemy;
    private State prevState;
    public SME1_FlipState(FiniteStateMachine stateMachine, Entity entity, string animName, SlowMutantElite1 enemy) : base(stateMachine, entity, animName)
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

    public void SetPrevState(State prevState) => this.prevState = prevState;

    public override void CompleteFlip()
    {
        enemy.Flip();
        if(prevState != null)
        {
            stateMachine.SwitchState(prevState);
            prevState = null;
        }
        else
        {
            stateMachine.SwitchState(enemy.walkState);
        }
    }
}
