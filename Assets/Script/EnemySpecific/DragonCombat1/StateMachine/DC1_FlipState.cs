using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_FlipState : FlipState
{
    protected DragonCombat1 enemy;
    protected State prevState;
    public DC1_FlipState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
    }
    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override void Complete()
    {
        base.Complete();
    }

    public void SetPrevState(State prevState) => this.prevState = prevState;
    public override void CompleteFlip()
    {
        if(this.prevState == null)
        {
            stateMachine.SwitchState(enemy.idleState);
        }
        else
        {
            stateMachine.SwitchState(this.prevState);
            // clear cache
            this.prevState = null;
        }
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

    public override void ResetTimer()
    {
        
    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
    }
}
