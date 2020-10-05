using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackState : PlayerAttackState
{
    #region CONTROL VARIABLES
    private bool isFirstAttack;
    #endregion
    public PlayerMeleeAttackState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {
    }

    public override void Enter()
    {
        base.Enter();
        combatData.damage = data.MAS_damageAmount;
        combatData.stunDamage = data.PD_maxStunPoint;
        combatData.knockbackDir = data.MAS_knockbackDirection;
        combatData.knockbackImpulse = data.MAS_knockbackImpulse;
    }

public override void Exit()
    {
        base.Exit();
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
        isFirstAttack = false;
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

    public override void ConsumeAttackBuffer()
    {
        base.ConsumeAttackBuffer();

        isMeleeAttack = false;
        player.InputHandler.ResetIsMeleeAttack();
    }

    public override void CheckHitbox()
    {
        base.CheckHitbox();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitbox.position, data.MAS_hitboxRadius, data.GD_whatIsEnemy);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                combatData.position = player.transform.position;
                collider.gameObject.SendMessage("Damage", combatData);
            }
        }
        // consuming MeleeAttack buffer
        player.InputHandler.ResetIsMeleeAttack();

        isFirstAttack = !isFirstAttack;
    }

    public override void CompleteAttack()
    {
        base.CompleteAttack();

        if (isFirstAttack)
        {
            workspace.Set(player.transform.position.x + player.offsetCalculator.localPosition.x * player.facingDirection, player.transform.position.y);
        }
        else
        {
            workspace.Set(player.transform.position.x + player.offsetCalculator.localPosition.x * player.facingDirection, player.transform.position.y);
        }

        player.SetPosition(workspace);

        stateMachine.SwitchState(player.idleState);
    }

    public override bool CheckEndAttack() => !isMeleeAttack;
}
