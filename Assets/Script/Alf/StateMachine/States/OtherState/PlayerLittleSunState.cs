using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLittleSunState : PlayerState
{
    private float timerMax = 0.1f;
    private float currentTimer = 0.0f;
    private GameManager gameManager;
    public PlayerLittleSunState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data) : base(stateMachine, player, defaultAnimCode, data)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
        
        player.SetVelocity(Vector2.zero);
        ResetControlVariables();
        player.InputHandler.ResetAll();
        player.ResetPlayerStatus();

        currentTimer = -1f;

        player.GetLittleSunHandler()?.littleSunMenu.Activate();
        player.GetLittleSunHandler()?.littleSunMenu.ResetMenu();
    }

    public override void Exit()
    {
        base.Exit();
        player.InputHandler.ResetAll();
        player.GetLittleSunHandler()?.littleSunMenu.Deactivate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(currentTimer >= 0f){
            currentTimer -= Time.unscaledDeltaTime;
        }
        else{
            ProcessInput();
            currentTimer = timerMax;
        }
    }

    protected void ProcessInput(){
        if(normMovementInput.y < 0){
            player.GetLittleSunHandler()?.littleSunMenu.SelectNext();
        }
        else if(normMovementInput.y > 0){
            player.GetLittleSunHandler()?.littleSunMenu.SelectPrevious();
        }
        else if(isInteraction){
            player.GetLittleSunHandler()?.OnLittleSunTravel();
        }
        else if(isPause){
            player.stateMachine.SwitchState(player.idleState);
        }

        player.InputHandler.ResetAll();
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
        isInteraction = false;
        isPause = false;
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
}
