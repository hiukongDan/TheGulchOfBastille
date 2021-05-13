using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_FlipState : FlipState
{
    protected DragonCombat1 enemy;
    protected State prevState;
    protected bool shouldFlip;
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
        shouldFlip = true;
    }

    public override void CompleteFlip()
    {
        stateMachine.SwitchState(enemy.idleState);
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Enter()
    {
        base.Enter();
        //enemy.anim.applyRootMotion = false;
        shouldFlip = false;
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.anim.applyRootMotion = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(shouldFlip){
            enemy.Flip();
            shouldFlip = false;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer()
    {
        base.ResetTimer();
    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
    }
}
