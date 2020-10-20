using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConverseState : PlayerState
{
    public PlayerConverseState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data) : base(stateMachine, player, defaultAnimCode, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
        isJump = false;
        player.InputHandler.ResetIsJump();

        if (detectShouldFlip())
        {
            player.Flip();
        }

        player.EndConversation += EndConversationHandler;
    }

    private bool detectShouldFlip()
    {
        if (player.GetNPCEventHandler() != null && player.GetNPCEventHandler().gameObject.transform.position.x > player.transform.position.x)
        {
            if (player.facingDirection < 0)
                return true;
        }
        else if(player.facingDirection > 0)
        {
            return true;
        }

        return false;
    }

    public override void Exit()
    {
        base.Exit();
        player.InputHandler.ResetAll();

        player.EndConversation -= EndConversationHandler;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isJump)
        {
            player.GetNPCEventHandler().OnNPCInteraction();
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

    private void EndConversationHandler()
    {
        stateMachine.SwitchState(player.idleState);
    }
}
