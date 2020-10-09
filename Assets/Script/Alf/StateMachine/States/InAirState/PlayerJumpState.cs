using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerJumpState : PlayerInAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {
        ResetJumpAmountLeft();
    }

    public override void Enter()
    {
        base.Enter();

        SetPlayerInitialSpeed();
        jumpAmountLeft--;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        stateMachine.SwitchState(player.inAirState);
        player.inAirState.SetPlayerJumpAmountLeft(jumpAmountLeft);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoCheck()
    {
        base.DoCheck();
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();
    }

    protected override void ResetControlVariables()
    {
        base.ResetControlVariables();

        isCoyoteTime = false;
    }

}
