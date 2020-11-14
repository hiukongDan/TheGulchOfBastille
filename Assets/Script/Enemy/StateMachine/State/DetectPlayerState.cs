using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerState : State
{
    protected DetectPlayerStateData data;

    public DetectPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DetectPlayerStateData stateData):base(stateMachine, entity, animBoolName)
    {
        this.data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        detectPlayerTrans = null;

        DetectPlayerInMaxAgro();
        DetectPlayerInMinAgro();
        DetectPlayerInMeleeAttackRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.rb.velocity = Vector2.zero;
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

    public void SetPlayerDetectedTrans(Transform playerDetected)
    {
        detectPlayerTrans = playerDetected;
    }
}
