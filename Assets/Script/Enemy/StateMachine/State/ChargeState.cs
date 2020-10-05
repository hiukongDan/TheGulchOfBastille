using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ChargeState : State
{
    protected ChargeStateData data;

    protected bool isChargeTimeOver;
    protected float chargeBeginTime;

    public ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, ChargeStateData stateData):base(stateMachine, entity, animBoolName)
    {
        data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
        DetectPlayerInMeleeAttackRange();
        DetectPlayerInMaxAgro();
    }

    public override void Enter()
    {
        base.Enter();
        chargeBeginTime = Time.time;
        isChargeTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > chargeBeginTime + data.chargeDuration)
        {
            isChargeTimeOver = true;
        }
        else
        {
            isChargeTimeOver = false;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoChecks();
    }

}
