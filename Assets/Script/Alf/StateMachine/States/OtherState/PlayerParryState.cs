using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    public bool IsParryValid { get; private set; }

    public PlayerParryState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.ResetIsParry();

        ResetControlVariables();
        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
        player.InputHandler.ResetAll();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoCheck()
    {
        base.DoCheck();
    }

    protected override void ResetControlVariables()
    {
        base.ResetControlVariables();

        IsParryValid = false;
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
        //normMovementInput = player.InputHandler.NormMovementInput;
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();
/*
        shouldFlip = player.CheckFlip();
        currentVelocity = player.Rb.velocity;*/
    }

    public void CompleteParry()
    {
        stateMachine.SwitchState(player.idleState);
    }

    public void EnterParryValid() => IsParryValid = true;
    public void ExitParryValid() => IsParryValid = false;

}
