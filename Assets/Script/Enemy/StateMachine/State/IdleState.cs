using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected IdleStateData data;

    protected float stateStartTime,
        idleDurationTime;

    protected bool flipAfterIdle;

    public IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, IdleStateData idleData): base(stateMachine, entity, animBoolName)
    {
        data = idleData;
    }

    public override void Enter()
    {
        base.Enter();
        DoChecks();

        stateStartTime = Time.time;
        idleDurationTime = Random.Range(data.idleTimeMin, data.idleTimeMax);
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        DoChecks();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        DetectPlayerInMinAgro();
    }
}
