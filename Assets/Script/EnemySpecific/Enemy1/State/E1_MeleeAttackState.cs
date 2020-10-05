using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttackState : MeleeAttackState
{
    protected Enemy1 enemy;
    public E1_MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitBoxPoint, Enemy1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitBoxPoint)
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
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isAttacking && !detectPlayerInMaxAgro)
        {
            stateMachine.SwitchState(enemy.lookForPlayerState);
        }
        else if(!isAttacking && detectPlayerInMaxAgro)
        {
            enemy.detectPlayerState.SetPlayerDetectedTrans(detectPlayerTrans);
            stateMachine.SwitchState(enemy.detectPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoMeleeAttack()
    {
        base.DoMeleeAttack();
    }

    public override void CompleteMeleeAttack()
    {
        base.CompleteMeleeAttack();
    }
}
