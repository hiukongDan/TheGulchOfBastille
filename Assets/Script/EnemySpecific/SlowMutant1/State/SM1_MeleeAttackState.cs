using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM1_MeleeAttackState : MeleeAttackState
{
    protected SlowMutant1 enemy;
    public SM1_MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, MeleeAttackStateData stateData, Transform hitBoxPoint, SlowMutant1 enemy) : base(stateMachine, entity, animBoolName, stateData, hitBoxPoint)
    {
        this.enemy = enemy;
    }

    public override void CompleteMeleeAttack()
    {
        base.CompleteMeleeAttack();
        stateMachine.SwitchState(enemy.fleeState);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void DoMeleeAttack()
    {
        base.DoMeleeAttack();
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

/*        if (!isAttacking)
        {
            //TODO: complicated options in future
            stateMachine.SwitchState(enemy.fleeState);
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
