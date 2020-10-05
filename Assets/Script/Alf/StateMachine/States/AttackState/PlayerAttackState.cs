using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    // TODO: STAMINA SYSTEM

    /*
     * RESETTING JUMP BUFFER WHEN EXIT THIS STATE AND CHILD STATE
     * CAN BE CHANGED IN THE FUTURE
     */

    #region INPUT SUBSCRIPTION
    protected Vector2 normMovementInput;
    protected bool isMeleeAttack;
    #endregion

    #region STATUS SUBSCRIPTION
    protected bool shouldFlip;
    protected Vector2 currentVelocity;
    #endregion

    #region PHYSICS STATUS SUBSCRIPTION
    protected bool isGrounded;
    #endregion

    #region CONTROL VARIABLES

    #endregion

    #region REFERENCES
    protected Transform hitbox;
    #endregion

    #region AUXILIARY VARIABLES
    protected CombatData combatData;
    #endregion
    public PlayerAttackState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        hitbox = player.hitbox;
        player.attackState = this;

        player.Rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();

        player.InputHandler.ResetAll();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
        isMeleeAttack = player.InputHandler.isMeleeAttack;
    }

    protected override void UpdatePhysicsStatusSubScription()
    {
        base.UpdatePhysicsStatusSubScription();

        isGrounded = player.CheckGrounded();
    }

    protected override void UpdateStatusSubscription()
    {
        base.UpdateStatusSubscription();

        shouldFlip = player.CheckFlip();
        currentVelocity = player.Rb.velocity;
    }

    public virtual void ConsumeAttackBuffer()
    {

    }

    public virtual void CheckHitbox()
    {

    }

    public virtual void CompleteAttack()
    {

    }

    public virtual bool CheckEndAttack()
    {
        return !isMeleeAttack;
    }

    // TODO: PROJECTILES ATTACK
}
