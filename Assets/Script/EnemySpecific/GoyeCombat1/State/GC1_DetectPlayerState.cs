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
        return enemy.evadeState.CanAction() || enemy.chargeState.CanAction() || enemy.defenceState.CanAction(); // || enemy.meleeAttackState.CanAction();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
