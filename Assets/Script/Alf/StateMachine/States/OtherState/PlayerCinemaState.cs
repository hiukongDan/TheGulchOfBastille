using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinemaState : PlayerState
{
    public PlayerCinemaState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data) : base(stateMachine, player, defaultAnimCode, data)
    {

    }

    public override bool CanAction()
    {
        return false;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);
    }

    public override void Exit()
    {
        base.Exit();
        player.InputHandler.ResetAll();
    }

    public void CompleteCinema()
    {
        stateMachine.SwitchState(player.idleState);
    }
}
