using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    protected float gravityOld;
    protected float dashCoolDownTimer;

    #region CONTROL VARIABLES
    public int dashAmountLeft { get; private set; }
    #endregion

    public PlayerDashState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data) : base(stateMachine, player, defaultAnimCode, data)
    {
        ResetDashAmountLeft();
        dashCoolDownTimer = -1;
    }

    public override void Enter()
    {
        base.Enter();

        gravityOld = player.Rb.gravityScale;
        player.Rb.gravityScale = 0;

        dashAmountLeft--;
        player.InputHandler.ResetIsRoll();

        player.SetVelocity(Vector2.zero);
    }

    public override void Exit()
    {
        base.Exit();
        player.Rb.gravityScale = gravityOld;

        ResetTimer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time > startTime + data.DS_dashTime)
        {
            player.stateMachine.SwitchState(player.inAirState);
        }
        else
        {
            workspace.Set(player.facingDirection * data.DS_dashSpeed, 0f);
            player.SetVelocity(workspace);
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

    public void ResetDashAmountLeft() => dashAmountLeft = data.DS_dashAmounts;

    public override void ResetTimer() => dashCoolDownTimer = data.DS_dashCoolDownTime;
    public override void UpdateTimer()
    {
        if(dashCoolDownTimer >= 0)
        {
            dashCoolDownTimer -= Time.deltaTime;
        }
    }

    public override bool CanAction()
    {
        return player.playerAbilityData.dash && dashAmountLeft > 0 && dashCoolDownTimer < 0;
    }
    
}
