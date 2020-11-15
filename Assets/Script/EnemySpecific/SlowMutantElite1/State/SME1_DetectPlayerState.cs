using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_DetectPlayerState : DetectPlayerState
{
    private SlowMutantElite1 enemy;
    private float maxAgroAttackRate = 0.2f;
    private float meleeRangeAttackRate = 0.5f;
    public SME1_DetectPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DetectPlayerStateData stateData, SlowMutantElite1 enemy) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        DoChecks();

        switch (enemy.currentStage)
        {
            case 0:
                StageOneLogicUpdate();
                break;
            case 1:
                StageTwoLogicUpdate();
                break;
            default:
                StageOneLogicUpdate();
                break;
        }
    }

    protected void StageOneLogicUpdate()
    {
        if (detectPlayerInMeleeRange)
        {
            // attack
            if (Random.value < meleeRangeAttackRate && enemy.heideAttackState.CanAction())
                stateMachine.SwitchState(enemy.heideAttackState);
            else if (enemy.evadeState.CanAction())
                stateMachine.SwitchState(enemy.evadeState);
            else
                stateMachine.SwitchState(enemy.flipState);
        }
        else if (detectPlayerInMaxAgro)
        {
            if (Random.value < maxAgroAttackRate && enemy.heideAttackState.CanAction())
                stateMachine.SwitchState(enemy.heideAttackState);
            else if(enemy.chargeState.CanAction())
                stateMachine.SwitchState(enemy.chargeState);
            else
                stateMachine.SwitchState(enemy.flipState);
        }
        else
        {
            stateMachine.SwitchState(enemy.flipState);
        }
    }

    protected void StageTwoLogicUpdate()
    {
        if (detectPlayerInMeleeRange && enemy.stageTwoHeideAttackState.CanAction())
        {
            stateMachine.SwitchState(enemy.stageTwoHeideAttackState);
        }
        else if(detectPlayerInMaxAgro && enemy.stageTwoTentacleAttackState.CanAction())
        {
            stateMachine.SwitchState(enemy.stageTwoTentacleAttackState);
        }
        else if(detectPlayerInMaxAgro)
        {
            stateMachine.SwitchState(enemy.stageTwoIdleState);
        }
        else
        {
            enemy.stageTwoFlipState.SetPrevState(enemy.stageTwoIdleState);
            stateMachine.SwitchState(enemy.stageTwoFlipState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override bool CanAction()
    {
        return (enemy.currentStage == 0 && (enemy.evadeState.CanAction() || enemy.heideAttackState.CanAction() || enemy.chargeState.CanAction())) ||
            (enemy.currentStage == 1 && (enemy.stageTwoHeideAttackState.CanAction() || enemy.stageTwoTentacleAttackState.CanAction()));
    }
}
