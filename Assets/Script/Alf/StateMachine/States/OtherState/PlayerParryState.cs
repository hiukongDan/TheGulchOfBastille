using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    #region INPUT SUBSCRIPTION
    protected bool isParryCanceled;
    #endregion

    public PlayerParryState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.ResetIsParry();
    }

    public override void Exit()
    {
        base.Exit();
        player.InputHandler.ResetAll();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (shouldFlip && canFlip)
        {
            player.Flip();
        }

        if (isParryCanceled || Time.time > startTime + data.PS_maxTime)
        {
            stateMachine.SwitchState(player.idleState);
        }
        else if (normMovementInput.x != 0)
        {
            player.SetVelocityX(normMovementInput.x * player.playerData.PS_horizontalSpeed);
        }
        else
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
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
        isParryCanceled = player.InputHandler.isParryCanceled;
        normMovementInput = player.InputHandler.NormMovementInput;
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();

        shouldFlip = player.CheckFlip();
        currentVelocity = player.Rb.velocity;
    }
}
