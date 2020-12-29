using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_DetectPlayerState : DetectPlayerState
{
    protected GoyeCombat1 enemy;
    public GC1_DetectPlayerState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DetectPlayerStateData stateData, GoyeCombat1 enemy) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override bool CanAction()
    {
        return enemy.evadeState.CanAction() || enemy.meleeAttackState.CanAction() || enemy.chargeState.CanAction() || enemy.defenceState.CanAction();
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
        
        // TODO: if enemy.IsPlayerWithinMeleeRange: defence
        if (detectPlayerInMeleeRange)
        {
            // attack | evade
            if(Random.value > 0.5 && enemy.meleeAttackState.CanAction())
            {
                stateMachine.SwitchState(enemy.meleeAttackState);
            }
            else if(enemy.evadeState.CanAction())
            {
                stateMachine.SwitchState(enemy.evadeState);
            }
        }
        else if (detectPlayerInMinAgro)
        {
            // defence
            if (enemy.defenceState.CanAction())
            {
                if(Random.value > 0.5)
                {
                    enemy.defenceState.SetIsDefenceForward(true);
                }
                else
                {
                    enemy.defenceState.SetIsDefenceForward(false);
                }
                stateMachine.SwitchState(enemy.defenceState);
            }
        }
        else if (detectPlayerInMaxAgro)
        {
            // charge
            if (enemy.chargeState.CanAction())
            {
                if (Random.value > 0.2)
                {
                    stateMachine.SwitchState(enemy.chargeState);
                }
                else
                {
                    stateMachine.SwitchState(enemy.runState);
                }
            }
        }
        else
        {
            stateMachine.SwitchState(enemy.runState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
