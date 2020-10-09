using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallState : PlayerState
{
    #region CONTROL VARIABLES
    public int wallJumpAmountLeft { get; private set; }
    #endregion

    public PlayerWallState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data) : base(stateMachine, player, defaultAnimCode, data)
    {
        ResetWallJumpAmountLeft();
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(Vector2.zero);
        SetPlayerInitialWallJumpImpulse();
        wallJumpAmountLeft--;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > startTime + data.WS_wallJumpHoldTime && currentVelocity.y < 0.01f)
        {
            stateMachine.SwitchState(player.inAirState);
        }
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
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();
    }

    public void ResetWallJumpAmountLeft() => wallJumpAmountLeft = data.WS_wallJumpAmounts;

    protected void SetPlayerInitialWallJumpImpulse()
    {
        player.Flip();
        workspace.Set(data.WS_wallJumpImpulse.x * player.facingDirection, data.WS_wallJumpImpulse.y);
        player.Rb.AddForce(workspace, ForceMode2D.Impulse);
        player.InputHandler.ResetIsJump();
    }
}
