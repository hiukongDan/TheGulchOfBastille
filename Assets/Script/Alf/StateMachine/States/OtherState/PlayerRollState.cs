using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    protected float rollCoolDownTimer;
    public PlayerRollState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {
        rollCoolDownTimer = -1f;
    }

    public override void Enter()
    { 
        base.Enter();
        player.InputHandler.ResetIsRoll();
    }

    public override void Exit()
    {
        base.Exit();
        player.InputHandler.ResetAll();

        player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.InputHandler.ResetIsParry();

        player.SetVelocityX(player.facingDirection * data.RS_rollSpeed);
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
        normMovementInput = player.InputHandler.NormMovementInput;
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();
    }

    public override void ResetTimer() => rollCoolDownTimer = data.RS_CoolDownTimer;
    public override bool CanAction() => rollCoolDownTimer <= 0;
    public override void UpdateTimer()
    {
        UpdateRollCoolDownTimer();
    }
    protected void UpdateRollCoolDownTimer()
    {
        if (rollCoolDownTimer >= 0)
            rollCoolDownTimer -= Time.deltaTime;
    }

    public void CompleteRoll()
    {
        /*        workspace.Set(player.transform.position.x + player.offsetCalculator.localPosition.x * player.facingDirection, player.transform.position.y);
                player.SetPosition(workspace);*/

        ResetTimer();
        stateMachine.SwitchState(player.idleState);
    }
}
