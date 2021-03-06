﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    protected override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // -- LOGIC BLOCK --------------------------------------------------------
        if (shouldFlip && canFlip)
        {
            player.Flip();
        }

        if (!isAction)
        {
            if (isJump)
            {
                player.stateMachine.SwitchState(player.jumpState);
            }
            else if (normMovementInput.x != 0)
            {
                if(animCode == AlfAnimationHash.IDLE_1)
                {
                    player.walkState.SetAnimationCode(AlfAnimationHash.RUN_1);
                }
                stateMachine.SwitchState(player.walkState);
            }
/*            else if (Mathf.Abs(currentVelocity.y) > 0.01f)
            {
                stateMachine.SwitchState(player.inAirState);
            }*/
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
