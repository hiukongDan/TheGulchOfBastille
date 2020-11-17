using UnityEngine;

public class SME1_FlipState : FlipState
{
    protected SlowMutantElite1 enemy;
    protected State prevState;
    protected State defaultPrevState;

    //protected bool isFlip;
    public SME1_FlipState(FiniteStateMachine stateMachine, Entity entity, string animName, State defaultPrevState, SlowMutantElite1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
        this.defaultPrevState = defaultPrevState;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        //isFlip = true;
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
    public void SetDefaultPrevState(State newDefaultPrevState) => defaultPrevState = newDefaultPrevState;
    public override void CompleteFlip()
    {
        if (prevState != null)
        {
            stateMachine.SwitchState(prevState);
            prevState = null;
        }
        else if (defaultPrevState != null)
        {
            stateMachine.SwitchState(defaultPrevState);
        }
        else
        {
            if (enemy.currentStage == 0)
                stateMachine.SwitchState(enemy.walkState);
            else
                stateMachine.SwitchState(enemy.stageTwoIdleState);
        }
    }
}
