using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    #region INPUT SUBSCRIPTION
    protected Vector2 normMovementInput;
    protected bool isJump;
    protected bool isJumpCanceled;
    #endregion

    #region STATUS SUBSCRIPTION
    protected bool shouldFlip;
    protected Vector2 currentVelocity;
    #endregion

    #region PHYSICS STATUS SUBSCRIPTION
    protected bool isGrounded;
    #endregion

    #region CONTROL VARIABLES
    protected int jumpAmountLeft;
    protected float tryJumpTimer;
    protected bool isTryJump;
    protected bool isCoyoteTime;
    #endregion

    public PlayerInAirState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

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

        player.Anim.SetFloat("yVelocity", currentVelocity.y);

        if (canFlip && shouldFlip)
        {
            player.Flip();
        }

        workspace.Set(normMovementInput.x * player.playerData.JS_horizontalSpeed, currentVelocity.y);
        player.SetVelocity(workspace);

        if (Time.time < data.GS_coyoteTime + startTime && isJump && isCoyoteTime)
        {
            isCoyoteTime = false;
            jumpAmountLeft--;
            SetPlayerInitialSpeed();
        }
        // AIR JUMP
        else if (jumpAmountLeft > 0 && isJump)
        {
            jumpAmountLeft--;
            SetPlayerInitialSpeed();
        }

        if (isGrounded && currentVelocity.y < 0.01f)
        {
            if (isTryJump)
            {
                player.jumpState.ResetJumpAmountLeft();
                SetPlayerInitialSpeed();
            }
            else if (normMovementInput.x != 0)
            {
                stateMachine.SwitchState(player.walkState);
            }
            else
            {
                stateMachine.SwitchState(player.idleState);
            }
            player.InputHandler.ResetIsJump();
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

        normMovementInput = player.InputHandler.NormMovementInput;
        isJump = player.InputHandler.isJump;
        isJumpCanceled = player.InputHandler.isJumpCanceled;
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();
        currentVelocity = player.Rb.velocity;
        shouldFlip = player.CheckFlip();
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
        isGrounded = player.CheckGrounded();
    }

    protected override void ResetControlVariables()
    {
        base.ResetControlVariables();
        isTryJump = false;
        isCoyoteTime = true;
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
