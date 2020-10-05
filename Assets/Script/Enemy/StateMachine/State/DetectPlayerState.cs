using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerState : State
{
    protected bool playerWithinAgroMin;
    protected bool playerWithinAgroMax;
    protected bool playerWithinMeleeRange;

    protected DetectPlayerStateData data;

    public DetectPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DetectPlayerStateData stateData):base(stateMachine, entity, animBoolName)
    {
        this.data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        playerWithinAgroMax = false;
        playerWithinAgroMin = false;
        playerWithinMeleeRange = false;

        entity.rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Mathf.Abs(detectPlayerTrans.position.x - entity.aliveGO.transform.position.x) < entity.entityData.meleeAttackDistance)
        {
            playerWithinMeleeRange = true;
        }
        else if (Mathf.Abs(detectPlayerTrans.position.x - entity.aliveGO.transform.position.x) < entity.entityData.detectPlayerAgroMinDistance)
        {
            playerWithinAgroMin = true;
        }
        else if (Mathf.Abs(detectPlayerTrans.position.x - entity.aliveGO.transform.position.x) < entity.entityData.detectPlayerAgroMaxDistance)
        {
            playerWithinAgroMax = true;
        }
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
