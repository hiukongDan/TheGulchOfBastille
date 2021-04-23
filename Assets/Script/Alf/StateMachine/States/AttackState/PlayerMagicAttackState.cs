using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicAttackState : PlayerAttackState
{
    protected bool isAttackCanceled;
    private float magicCooldownTimer;
    protected Vector2 mousePosition;
    private Vector2 direction;
    // sprotected bool isCasting;
    public PlayerMagicAttackState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {
        magicCooldownTimer = -1f;
    }

    public override bool CanAction() => magicCooldownTimer < 0;

    public override bool CheckEndAttack()
    {
        return base.CheckEndAttack();
    }

    public override void CheckHitbox()
    {
        // TODO:  cast magic ball here
        switch(player.playerRuntimeData.GetCurrentWeaponInfo().weapon){
            case ItemData.Weapon.Apprentice_Stick:
                ShootBall(data.MAS_apprenticeMagicBall);
                break;
            case ItemData.Weapon.Master_Stick:
                Debug.Log("Master stick attacking");
                break;
            case ItemData.Weapon.Sunlight_Stick:
                ShootBall(data.MAS_sunlightMagicBall);
                break;
            default:
                ShootBall(data.MAS_apprenticeMagicBall);
                break;
        }
    }

    protected void ShootBall(GameObject ballPref){

    }

    public override void CompleteAttack()
    {
        player.idleState.SetAnimationCodeFromWeapon();
        player.stateMachine.SwitchState(player.idleState);
    }

    public override void ConsumeAttackBuffer()
    {
        base.ConsumeAttackBuffer();
    }

    public override void Enter()
    {
        switch(player.playerRuntimeData.GetCurrentWeaponInfo().weapon){
            case ItemData.Weapon.Apprentice_Stick:
                animCode = AlfAnimationHash.ATTACK_APPRENTICE_STICK;
                break;
            case ItemData.Weapon.Master_Stick:
                animCode = AlfAnimationHash.ATTACK_MASTER_STICK;
                break;
            case ItemData.Weapon.Sunlight_Stick:
                animCode = AlfAnimationHash.ATTACK_SUNLIGHT_STICK;
                break;
            default:
                animCode = AlfAnimationHash.ATTACK_APPRENTICE_STICK;
                break;
        }
        base.Enter();

        combatData.damage = player.CalculatePlayerDamage();
        combatData.stunDamage = data.MAS_stunAmount;
        combatData.knockbackDir = data.MAS_knockbackDirection;
        combatData.knockbackImpulse = data.MAS_knockbackImpulse;
        combatData.from = null;
        combatData.isParryDamage = false;
        combatData.facingDirection = player.facingDirection;

        ResetControlVariables();
        RequireDirection();
    }

    protected void RequireDirection(){
        var dir = Camera.main.ScreenToWorldPoint(mousePosition) - player.transform.position;
        direction = new Vector2(dir.x, dir.y).normalized;
        if(normMovementInput != Vector2.zero){
            direction = normMovementInput;
        }
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

    public override void ResetTimer(){
        magicCooldownTimer = data.MAS_attackCooldownTimer;
        if(player.playerRuntimeData.playerSlot.IsWearableEquiped(player.playerRuntimeData.playerStock, ItemData.Wearable.Coldblue_Ring)){
            magicCooldownTimer *= ItemData.WearableItemBuffData.Coldblue_Ring_attackReductionMultiplier;
        }
    }

    public override void UpdateTimer()
    {
        if(magicCooldownTimer >= 0){
            magicCooldownTimer -= Time.deltaTime;
        }
    }

    protected override void DoCheck()
    {
        base.DoCheck();
    }

    protected override void ResetControlVariables()
    {
        base.ResetControlVariables();

        // isCasting = true;
    }

    protected override void UpdateInputSubscription()
    {
        base.UpdateInputSubscription();
        isAttackCanceled = player.InputHandler.isMeleeAttackCanceled;
        mousePosition = player.InputHandler.MousePosInput;
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
