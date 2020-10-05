using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM1_DeadState : DeadState
{
    protected SlowMutant1 enemy;
    public SM1_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DeadStateData stateData, SlowMutant1 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
        
        GameObject.Destroy(entity.gameObject,  data.disappearTime);
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
