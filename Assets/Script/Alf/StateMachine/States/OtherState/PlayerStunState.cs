using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunState : PlayerState
{
    #region CONTROL VARIABLES
    protected bool isStun;
    #endregion


    public PlayerStunState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {
        player.Anim.SetBool("stun", true);
        ResetControlVariables();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        player.Anim.SetBool("stun", true);
        ResetControlVariables();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > startTime + data.SS_stunDurationTime && isStun)
        {
            CompleteStunHold();
            isStun = false;
        }

        if (isGrounded && currentVelocity.y < 0.01f && isStun)
        {
            player.SetVelocityX(0);
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

        isStun = true;
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();

        isGrounded = player.CheckGrounded();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();

        currentVelocity = player.Rb.velocity;
    }

    public void CompleteStunHold() => player.Anim.SetBool("stun", false);

    public void ComplementStun()
    {
        stateMachine.SwitchState(player.idleState);
    }
}
