﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    #region INPUT SUBSCRIPTION
    protected Vector2 normMovementInput;
    #endregion
    public PlayerRollState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.ResetIsRoll();
    }

    public override void Exit()
    {
        base.Exit();
        player.InputHandler.ResetAll();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.InputHandler.ResetIsParry();
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
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
        normMovementInput = player.InputHandler.NormMovementInput;
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();
    }

    public void CompleteRoll()
    {
        workspace.Set(player.transform.position.x + player.offsetCalculator.localPosition.x * player.facingDirection, player.transform.position.y);
        player.SetPosition(workspace);

        stateMachine.SwitchState(player.idleState);
    }
}
