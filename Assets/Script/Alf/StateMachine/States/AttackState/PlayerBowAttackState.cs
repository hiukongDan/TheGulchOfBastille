using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAttackState : PlayerAttackState
{
    protected bool isAttackCanceled;
    protected bool isShooting;
    protected bool isCharging;
    protected Vector2 mousePosition;
    protected float bowCoolDownTimer;
    public PlayerBowAttackState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {
        this.bowCoolDownTimer = -1;
    }

    public override bool CanAction() => bowCoolDownTimer < 0;

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
        if(isCharging){
            isCharging = false;
            switch(player.playerRuntimeData.GetCurrentWeaponInfo().weapon){
                case ItemData.Weapon.Wood_Bow:
                    player.Anim.Play(AlfAnimationHash.BOW_AIM_WOODBOW);
                    break;
                case ItemData.Weapon.Elf_Bow:
                    player.Anim.Play(AlfAnimationHash.BOW_AIM_ELFBOW);
                    break;
                case ItemData.Weapon.Long_Bow:
                    player.Anim.Play(AlfAnimationHash.BOW_AIM_LONGBOW);
                    break;
                default:
                    player.Anim.Play(AlfAnimationHash.BOW_AIM_WOODBOW);
                    break;
            }
        }
        else{
            player.idleState.SetAnimationCodeFromWeapon();
            player.stateMachine.SwitchState(player.idleState);
        }
    }

    public override void ConsumeAttackBuffer()
    {
        base.ConsumeAttackBuffer();
    }

    public override void Enter()
    {
        //base.Enter();
        //player.Anim.Play(AlfAnimationHash.BOW_AIM_WOODBOW);

        switch(player.playerRuntimeData.GetCurrentWeaponInfo().weapon){
            case ItemData.Weapon.Wood_Bow:
                animCode = AlfAnimationHash.BOW_CHARGE_WOODBOW;
                break;
            case ItemData.Weapon.Elf_Bow:
                animCode = AlfAnimationHash.BOW_CHARGE_ELFBOW;
                break;
            case ItemData.Weapon.Long_Bow:
                animCode = AlfAnimationHash.BOW_CHARGE_LONGBOW;
                break;
            default:
                animCode = AlfAnimationHash.BOW_CHARGE_WOODBOW;
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

        ConsumeAttackBuffer();
        ResetControlVariables();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(isCharging){
            SetAnimRadians();
        }
        else if(isAttackCanceled && !isShooting){
            // shoot
            switch(player.playerRuntimeData.GetCurrentWeaponInfo().weapon){
                case ItemData.Weapon.Wood_Bow:
                    player.Anim.Play(AlfAnimationHash.BOW_SHOOT_WOODBOW);
                    break;
                case ItemData.Weapon.Elf_Bow:
                    player.Anim.Play(AlfAnimationHash.BOW_SHOOT_ELFBOW);
                    break;
                case ItemData.Weapon.Long_Bow:
                    player.Anim.Play(AlfAnimationHash.BOW_SHOOT_LONGBOW);
                    break;
                default:
                    player.Anim.Play(AlfAnimationHash.BOW_SHOOT_WOODBOW);
                    break;
            }
            isShooting = true;
            ShootArrow();
        }
        else{
            SetAnimRadians();
        }

    }

    protected void SetAnimRadians(){
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 dir = (pos - new Vector2(player.transform.position.x, player.transform.position.y)).normalized;
        if(normMovementInput != Vector2.zero){
            // if there is controller input
            dir = normMovementInput;
        }
        
        if(Mathf.Sign(dir.x) != player.facingDirection){
            player.Flip();
        }
        if(MapRadiansFromDirection(dir) != -2){
            player.Anim.SetFloat("aim_radians", MapRadiansFromDirection(dir));
        }
    }

    protected void ShootArrow(){
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 dir = (pos - new Vector2(player.transform.position.x, player.transform.position.y)).normalized;
        if(normMovementInput != Vector2.zero){
            // if there is controller input
            dir = normMovementInput;
        }

        var arrow = GameObject.Instantiate(data.BAS_arrowPrefab,
            player.transform.position,
            Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg),
            player.transform.parent);
        arrow.GetComponentInChildren<Arrow>().SetCombatData(combatData);
    }

    protected float MapRadiansFromDirection(Vector2 dir){
        float ret = -2;
        if(dir.y > -0.9 && dir.y < 0.9){
            ret = Mathf.Atan2(dir.y, Mathf.Abs(dir.x));
        }
        else{
            ret = -2;
        }
        return ret;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer()
    {
        bowCoolDownTimer = data.MAS_attackCooldownTimer;
        if(player.playerRuntimeData.playerSlot.IsWearableEquiped(player.playerRuntimeData.playerStock, ItemData.Wearable.Coldblue_Ring)){
            bowCoolDownTimer *= ItemData.WearableItemBuffData.Coldblue_Ring_attackReductionMultiplier;
        }
    }

    public override void UpdateTimer()
    {
        if(bowCoolDownTimer >= 0){
            bowCoolDownTimer -= Time.deltaTime;
        }
    }

    protected override void DoCheck()
    {
        base.DoCheck();
    }

    protected override void ResetControlVariables()
    {
        base.ResetControlVariables();
        isShooting = false;
        isCharging = true;
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
