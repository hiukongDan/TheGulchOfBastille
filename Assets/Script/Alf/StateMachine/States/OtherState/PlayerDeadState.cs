using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    #region REFERENCES
    protected GameManager GM;
    #endregion

    #region CONTROL VARIABLES
    protected bool isDead = false;
    #endregion

    public PlayerDeadState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data) : base(stateMachine, player, defaultAnimCode, data)
    {
        GM = GameObject.Find("GameManager")?.GetComponent<GameManager>();
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

        player.SetVelocityX(0);
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

    public void CompleteDead()
    {
        Gulch.GameEventListener.Instance.OnPlayerDead();
        if(player.playerRuntimeData.playerSlot.IsWearableEquiped(player.playerRuntimeData.playerStock, ItemData.Wearable.Sun_Protection_Stone)){
            if(Random.value > ItemData.WearableItemBuffData.Sun_Protection_Stone_decayPointNotIncreasedRate){
                player.playerRuntimeData.currentDecayPoints++;
            }
        }
        else{
            player.playerRuntimeData.currentDecayPoints++;
        }
        
        EnemySaveData.ResetRevivableEnemy();
        if(player.playerRuntimeData.currentDecayPoints >= player.playerData.PD_maxDecayPoint){
            GameObject.Find("GameManager").GetComponent<GameManager>().playerCinemaMovement.TransitToBelial();
        }
        else{
            GM?.ReloadScene();
        }

    }
}
