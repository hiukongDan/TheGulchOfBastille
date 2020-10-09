using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerWalkState : PlayerGroundState
{

    public PlayerWalkState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }
    protected override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (shouldFlip && canFlip)
        {
            player.Flip();
        }

        if (isMeleeAttack || isRoll || isParry)
        {
            player.SetVelocityX(0f);
        }

        if (!isAction)
        {
            if (isGrounded && normMovementInput.x == 0)
            {
                stateMachine.SwitchState(player.idleState);
            }
            else if (isGrounded && normMovementInput.x != 0)
            {
                workspace.Set(normMovementInput.x * data.WS_walkSpeed, 0f);
                player.SetVelocity(workspace);
            }
            else if (!isGrounded)
            {
                stateMachine.SwitchState(player.inAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
