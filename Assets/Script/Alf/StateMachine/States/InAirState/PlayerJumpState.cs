using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerJumpState : PlayerInAirState
{
    #region CONTROL VARIABLES
    protected bool C_isJumpCanceled;
    #endregion

    public PlayerJumpState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        isCoyoteTime = false;

        SetPlayerInitialSpeed();
        ResetControlVariables();

        jumpAmountLeft--;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // -- LOGIC BLOCK ------------------------------------------------------------
        if (currentVelocity.y > 0 && !C_isJumpCanceled && isJumpCanceled)
        {
            C_isJumpCanceled = true;
            
            workspace.Set(currentVelocity.x, currentVelocity.y * player.playerData.JS_jumpCanceledMultiplier);
            player.SetVelocity(workspace);
        }
        else
        {
            stateMachine.SwitchState(player.inAirState);
            player.inAirState.SetPlayerJumpAmountLeft(jumpAmountLeft);
        }
        // ---------------------------------------------------------------------------
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

        C_isJumpCanceled = false;
    }

}
