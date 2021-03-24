using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{

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

        isAction = true;

        // NOTE: JUMP BUTTON SHARES WITH INTERACTION BUTTON
        if (isJump)
        {
            stateMachine.SwitchState(player.jumpState);
        }
        else if(isInteraction)
        {
            if(player.GetSubAreaHandler() != null)
            {
                player.GetSubAreaHandler().OnPerformAction();
            }
            else if (player.GetNPCEventHandler() != null)
            {
                player.GetNPCEventHandler().OnNPCInteraction();
                stateMachine.SwitchState(player.converseState);
            }
            else if(player.GetLittleSunHandler() != null)
            {
                player.GetLittleSunHandler().OnLittleSunInteraction();
            }
            else if(player.ladderState.HasValidLadder()){
                stateMachine.SwitchState(player.ladderState);
            }
            else if(player.GetInteractable() != null){
                player.GetInteractable().OnInteraction();
            }
            else
            {
                player.InputHandler.ResetIsInteraction();
            }
        }
        else if (isMeleeAttack && player.meleeAttackState.CanAction())
        {
            stateMachine.SwitchState(player.meleeAttackState);
        }
        else if (isParry && player.parryState.CanAction())
        {
            stateMachine.SwitchState(player.parryState);
        }
        else if (isRoll && player.rollState.CanAction())
        {
            stateMachine.SwitchState(player.rollState);
        }
        else
        {
            isAction = false;
            player.InputHandler.ResetAll();
        }
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
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
