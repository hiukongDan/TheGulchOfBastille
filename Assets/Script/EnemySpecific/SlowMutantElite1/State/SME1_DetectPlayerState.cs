using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_DetectPlayerState : DetectPlayerState
{
    private SlowMutantElite1 enemy;
    private float minAgroAttackRate = 0.5f;
    private float meleeRangeAttackRate = 0.2f;
    public SME1_DetectPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DetectPlayerStateData stateData, SlowMutantElite1 enemy) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectPlayerInMaxAgro();
    }

    public override void Enter()
    {
        //base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (playerWithinMeleeRange)
        {
            // attack
            if (Random.value < meleeRangeAttackRate)
                stateMachine.SwitchState(enemy.heideAttackState);
            else
                stateMachine.SwitchState(enemy.evadeState);
        }
        else if (playerWithinAgroMin)
        {
            if (Random.value < minAgroAttackRate)
                stateMachine.SwitchState(enemy.heideAttackState);
            else
                stateMachine.SwitchState(enemy.evadeState);
        }
        else if (playerWithinAgroMax)
        {
            // TODO: charge
            stateMachine.SwitchState(enemy.walkState);
        }
        else
        {
            // turn
            stateMachine.SwitchState(enemy.flipState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
