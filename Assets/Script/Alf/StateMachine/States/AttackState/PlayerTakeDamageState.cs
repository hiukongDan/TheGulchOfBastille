using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamageState : PlayerState
{
    protected float takeDamageTime;

    public PlayerTakeDamageState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data) : base(stateMachine, player, defaultAnimCode, data)
    {

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

        if (Time.time > startTime + takeDamageTime)
        {
            if (isGrounded && currentVelocity.y < 0.01f)
            {
                stateMachine.SwitchState(player.idleState);
            }
        }
        else if (isGrounded && currentVelocity.y < 0.01f)
        {
            player.SetVelocityX(0f);
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
        isGrounded = player.CheckGrounded();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();

        currentVelocity = player.Rb.velocity;
    }

    public void SetTakeDamageTime(float time) => takeDamageTime = time;
}
