using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected LookForPlayerStateData data;

    protected int turnTimesLeft;
    protected float lastTurnTime;
    protected bool waitTimeOver;

    public LookForPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, LookForPlayerStateData stateData) : base(stateMachine, entity, animBoolName)
    {
        data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectPlayerInMinAgro();
    }

    public override void Enter()
    {
        base.Enter();
        DoChecks();

        entity.rb.velocity = Vector2.zero;

        turnTimesLeft = data.totalTurnTimes;
        lastTurnTime = Time.time;

        waitTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(Time.time > lastTurnTime + data.waitTime && turnTimesLeft > 0)
        {
            waitTimeOver = true;
            lastTurnTime = Time.time;
            turnTimesLeft--;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoChecks();
    }
}
