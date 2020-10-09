using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackState : PlayerAttackState
{
    #region CONTROL VARIABLES
    private bool isFirstAttack;
    #endregion

    #region MISC
    private Vector2 oldPosition;
    #endregion

    public PlayerMeleeAttackState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {
    }

    public override void Enter()
    {
        base.Enter();
        combatData.damage = data.MAS_damageAmount;
        combatData.stunDamage = data.MAS_stunAmount;
        combatData.knockbackDir = data.MAS_knockbackDirection;
        combatData.knockbackImpulse = data.MAS_knockbackImpulse;

        oldPosition = player.transform.position;
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

                var particle = GameObject.Instantiate(data.MAS_meleeAttackParticle, hitbox.position, data.PS_particle.transform.rotation);
                particle.gameObject.transform.Rotate(0, 0, Random.Range(0, 360));
                particle.GetComponent<Animator>().Play(Random.Range(0, 3).ToString());
            }
        }
        // consuming MeleeAttack buffer
        player.InputHandler.ResetIsMeleeAttack();

        isFirstAttack = !isFirstAttack;
    }

    public override void CompleteAttack()
    {
        base.CompleteAttack();
        RaycastHit2D hit = Physics2D.Raycast(player.wallCheck.position, player.transform.right, player.offsetCalculator.localPosition.x, data.GD_whatIsGround);

        if (!hit.collider)
        {
            if (isFirstAttack)
            {
                workspace.Set(player.transform.position.x + player.offsetCalculator.localPosition.x * player.facingDirection, player.transform.position.y);
            }
            else
            {
                workspace.Set(player.transform.position.x + player.offsetCalculator.localPosition.x * player.facingDirection, player.transform.position.y);
            }
            
        }
        else
        {
            workspace = oldPosition;
        }
        
        player.SetPosition(workspace);
        stateMachine.SwitchState(player.idleState);
    }

    public override bool CheckEndAttack() => !isMeleeAttack;
}
