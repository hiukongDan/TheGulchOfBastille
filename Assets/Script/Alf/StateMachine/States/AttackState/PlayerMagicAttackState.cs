using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicAttackState : PlayerAttackState
{
    protected bool isAttackCanceled;
    public PlayerMagicAttackState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override bool CheckEndAttack()
    {
        return base.CheckEndAttack();
    }

    public override void CheckHitbox()
    {
        base.CheckHitbox();
    }

    public override void CompleteAttack()
    {
        base.CompleteAttack();
    }

    public override void ConsumeAttackBuffer()
    {
        base.ConsumeAttackBuffer();
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

        if(isAttackCanceled){
            Debug.Log("Perform Magic");
            player.idleState.SetAnimationCodeFromWeapon();
            player.stateMachine.SwitchState(player.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer()
    {
        base.ResetTimer();
    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
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
        isAttackCanceled = player.InputHandler.isMeleeAttackCanceled;
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
