using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerConverseState : PlayerState
{
    private float selectionTimer;
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

        selectionTimer = data.CS_selectionTimer;
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

        if(normMovementInput.y != 0 && selectionTimer < 0)
        {
            player.GetNPCEventHandler().OnNPCSelection((int)normMovementInput.y);
            selectionTimer = data.CS_selectionTimer;
        }
        else if(selectionTimer >= 0)
        {
            selectionTimer -= Time.deltaTime;
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
