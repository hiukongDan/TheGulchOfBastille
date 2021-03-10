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
    protected bool isLanded;
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
        else if(player.dashState.CanAction() &&  isRoll && !isGrounded && !isWalled)
        {
            stateMachine.SwitchState(player.dashState);
        }
        // WALL JUMP
        else if (player.wallState.CanWallJump() && isWalled && isJump && !isGrounded)
        {
            stateMachine.SwitchState(player.wallState);
        }
        // AIR JUMP
        else if (player.jumpState.CanDoubleJump() && isJump)
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
            if (isTryJump)
            {
                player.ResetGrounded();
                stateMachine.SwitchState(player.jumpState);
            }
            else if(!isLanded){
                if(!player.Anim.GetBool("landing")){
                    player.ResetGrounded();
                    player.Anim.SetBool("landing", true);
                    player.Anim.Play(AlfAnimationHash.LANDING_0);
                }
                workspace.Set(normMovementInput.x * player.playerData.JS_horizontalSpeed, currentVelocity.y);
                player.SetVelocity(workspace);
            }
            else{
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
        isLanded = false;
        player.Anim.SetBool("landing", false);
    }

    public void ResetJumpAmountLeft() => jumpAmountLeft = data.IAS_jumpAmounts;

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

    #region ANIMATION_CALLBACK
    public void CompleteLanding() => isLanded = true;
    #endregion

}
