using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipState : State
{
    public FlipState(FiniteStateMachine stateMachine, Entity entity, string animName) : base(stateMachine, entity, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.objectToAlive.flipState = this;
    }

    public override void Exit()
    {
        base.Exit();
        entity.objectToAlive.flipState = null;
    }

    public virtual void CompleteFlip()
    {

    }
}
