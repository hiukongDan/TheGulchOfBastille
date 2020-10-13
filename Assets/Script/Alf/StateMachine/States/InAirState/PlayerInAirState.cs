using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInAirState : PlayerState
{

    #region CONTROL VARIABLES
    protected int jumpAmountLeft;
    protected float tryJumpTimer;
    protected bool isTryJump;
    protected bool isCoyoteTime;
    protected bool C_isJumpCanceled;
    #endregion

    public PlayerInAirState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    public override void Enter()
    {
        base.Enter();

        ResetControlVariables();
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", currentVelocity.y);

        if (canFlip && shouldFlip)
        {
            player.Flip();
        }

        if (Time.time < data.GS_coyoteTime + startTime && isJump && isCoyoteTime)
        {
            isCoyoteTime = false;
            stateMachine.SwitchState(player.jumpState);
        }
        // DASH
        else if(player.dashState.dashAmountLeft > 0 && player.dashState.IsDashCoolDownComplete() &&  isRoll && !isGrounded && !isWalled)
        {
            stateMachine.SwitchState(player.dashState);
        }
        // WALL JUMP
        else if (player.wallState.wallJumpAmountLeft > 0 && isWalled && isJump && !isGrounded)
        {
            stateMachine.SwitchState(player.wallState);
        }
        // AIR JUMP
        else if (player.jumpState.jumpAmountLeft > 0 && isJump)
        {
            stateMachine.SwitchState(player.jumpState);
        }
        // CANCEL JUMP
        else if(currentVelocity.y > 0 && !C_isJumpCanceled && isJumpCanceled)
        {
            C_isJumpCanceled = true;

            workspace.Set(currentVelocity.x, currentVelocity.y * data.JS_jumpCanceledMultiplier);
            player.SetVelocity(workspace);
        }
        else if (isGrounded && Mathf.Abs(currentVelocity.y) < 0.01f)
        {
            player.InputHandler.ResetIsJump();
            player.jumpState.ResetJumpAmountLeft();
            player.wallState.ResetWallJumpAmountLeft();
            player.dashState.ResetDashAmountLeft();

            if (isTryJump)
            {
                stateMachine.SwitchState(player.jumpState);
            }
            else if (normMovementInput.x != 0)
            {
                stateMachine.SwitchState(player.walkState);
            }
            else
            {
                stateMachine.SwitchState(player.idleState);
            }
        }
        else
        {
            workspace.Set(normMovementInput.x * player.playerData.JS_horizontalSpeed, currentVelocity.y);
            player.SetVelocity(workspace);

            player.InputHandler.ResetIsRoll();
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void DoCheck()
    {
        base.DoCheck();
        CheckJumpBuffer();
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
    }

    protected override void ResetControlVariables()
    {
        base.ResetControlVariables();
        isTryJump = false;
        isCoyoteTime = true;
        C_isJumpCanceled = false;
    }

    protected void ResetJumpAmountLeft() => jumpAmountLeft = data.IAS_jumpAmounts;

    protected virtual void CheckJumpBuffer()
    {
        if (!isTryJump && isJump)
        {
            isTryJump = true;
            tryJumpTimer = data.IAS_jumpTryTime;
            player.InputHandler.ResetIsJump();
        }
        else if (tryJumpTimer > 0)
        {
            tryJumpTimer -= Time.deltaTime;
        }

        if (tryJumpTimer <= 0)
        {
            isTryJump = false;
        }
    }

    protected void SetPlayerInitialSpeed()
    {
        workspace.Set(normMovementInput.x * data.JS_horizontalSpeed, player.playerData.JS_jumpSpeed);
        player.SetVelocity(workspace);
        player.InputHandler.ResetIsJump();
    }

    public void SetPlayerJumpAmountLeft(int jumpAmount)
    {
        this.jumpAmountLeft = jumpAmount;
    }

}
