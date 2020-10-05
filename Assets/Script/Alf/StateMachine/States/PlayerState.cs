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

    public PlayerState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animCodeDefault = this.animCode = animCode;
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

    }

    protected virtual void UpdateStatusSubscription()
    {

    }

    protected virtual void UpdatePhysicsStatusSubScription()
    {

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

    /* TODO:
     * FOR ANIMATION CALLBACKS
        public virtual void CompleteAction() 
     */
}

// TODO: A timer system