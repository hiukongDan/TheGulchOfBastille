using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    #region INPUT SUBSCRIPTION
    protected Vector2 normMovementInput;
    protected bool isJump;
    protected bool isParry;
    protected bool isRoll;
    protected bool isMeleeAttack;
    #endregion

    #region STATUS SUBSCRIPTION
    protected bool shouldFlip;
    protected Vector2 currentVelocity;
    #endregion

    #region PHYSICS SUBSCRIPTION
    protected bool isGrounded;
    #endregion

    #region CONTROL VARIABLES
    protected bool isAction;
    #endregion

    public PlayerGroundState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    protected override void DoCheck()
    {
        base.DoCheck();
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

        if (isJump)
        {
            stateMachine.SwitchState(player.jumpState);
            isAction = true;
        }
        else if (isMeleeAttack)
        {
            stateMachine.SwitchState(player.meleeAttackState);
            isAction = true;
        }
        else if (isParry)
        {
            stateMachine.SwitchState(player.parryState);
            isAction = true;
        }
        else if (isRoll)
        {
            stateMachine.SwitchState(player.rollState);
            isAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();

        normMovementInput = player.InputHandler.NormMovementInput;
        isJump = player.InputHandler.isJump;
        isParry = player.InputHandler.isParry;
        isRoll = player.InputHandler.isRoll;
        isMeleeAttack = player.InputHandler.isMeleeAttack;
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();

        shouldFlip = player.CheckFlip();
        currentVelocity = player.Rb.velocity;
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
        isGrounded = player.CheckGrounded();
    }

    protected override void ResetControlVariables()
    {
        base.ResetControlVariables();

        isAction = false;
    }
}
