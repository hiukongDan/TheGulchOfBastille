using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected int animCode;
    private int animCodeDefault;

    protected PlayerStateMachine stateMachine;

    protected Player player;

    protected float startTime;

    protected bool canFlip = true;

    protected D_PlayerStateMachine data;

    protected Vector2 workspace;

    #region INPUT SUBSCRIPTION
    protected Vector2 normMovementInput;
    protected bool isJumpCanceled;
    protected bool isJump;
    protected bool isParry;
    protected bool isRoll;
    protected bool isMeleeAttack;
    protected bool isInteraction;
    #endregion

    #region PHYSICS STATUS SUBSCRIPTION
    protected bool isGrounded;
    protected bool isWalled;
    #endregion

    #region STATUS SUBSCRIPTION
    protected bool shouldFlip;
    protected Vector2 currentVelocity;
    #endregion

    public PlayerState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animCodeDefault = this.animCode = defaultAnimCode;
        this.data = data;
    }

    public virtual void Enter()
    {
        DoCheck();
        UpdateInputSubscription();
        UpdateStatusSubscription();
        UpdatePhysicsStatusSubScription();

        player.Anim.Play(animCode);
        startTime = Time.time;
}

    public virtual void Exit()
    {
        animCode = animCodeDefault;
    }

    public virtual void LogicUpdate()
    {
        UpdateInputSubscription();
        UpdateStatusSubscription();
    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
        UpdatePhysicsStatusSubScription();
    }

    protected virtual void DoCheck()
    {
        
    }

    protected virtual void UpdateInputSubscription()
    {
        normMovementInput = player.InputHandler.NormMovementInput;
        isJump = player.InputHandler.isJump;
        isParry = player.InputHandler.isParry;
        isRoll = player.InputHandler.isRoll;
        isMeleeAttack = player.InputHandler.isMeleeAttack;
        isJumpCanceled = player.InputHandler.isJumpCanceled;
        isInteraction = player.InputHandler.isInteraction;
    }

    protected virtual void UpdateStatusSubscription()
    {
        shouldFlip = player.CheckFlip();
        currentVelocity = player.Rb.velocity;
    }

    protected virtual void UpdatePhysicsStatusSubScription()
    {
        isGrounded = player.CheckGrounded();
        isWalled = player.CheckWalled();
    }

    protected void SetCanFlip(bool flip)
    {
        canFlip = flip;
    }

    protected virtual void ResetControlVariables()
    {

    }

    public void SetAnimationCode(int animCode)
    {
        this.animCode = animCode;
    }

    public virtual void UpdateTimer()
    {

    }

    public virtual bool CanAction()
    {
        return true;
    }

    public virtual void ResetTimer()
    {

    }

    /* TODO:
     * FOR ANIMATION CALLBACKS
        public virtual void CompleteAction() 
     */
}

// TODO: A timer system