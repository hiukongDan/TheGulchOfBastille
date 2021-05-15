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

    // used by animation event
    public override void CheckHitbox()
    {
        // TODO:  cast magic ball here
        ItemData.Weapon weapon = player.playerRuntimeData.GetCurrentWeaponInfo().weapon;
        switch(weapon){
            case ItemData.Weapon.Apprentice_Stick:
                ShootBall(data.MAS_apprenticeMagicBall);
                //Debug.Log("apprentice");
                break;
            case ItemData.Weapon.Master_Stick:
                ShootBall(data.MAS_masterMagicBall);
                //Debug.Log("master");
                break;
            case ItemData.Weapon.Sunlight_Stick:
                ShootBall(data.MAS_sunlightMagicBall);
                // Debug.Log("sunlight");
                break;
            default:
                ShootBall(data.MAS_apprenticeMagicBall);
                break;
        }
    }

    protected void ShootBall(GameObject ballPref){
        var ball = GameObject.Instantiate(ballPref, player.transform.position + new Vector3(0.5f, 0, 0), player.transform.rotation, player.transform.parent);
        var magicBall = ball.GetComponentInChildren<MagicBall>();
        magicBall.SetCombatData(combatData);
        magicBall.SetDirection(direction);
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
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 dir = (pos - new Vector2(player.transform.position.x, player.transform.position.y)).normalized;
        if(normNavigationInput != Vector2.zero){
            // if there is controller input
            dir = normNavigationInput;
        }
        
        if(Mathf.Sign(dir.x) != player.facingDirection){
            player.Flip();
        }
        
        direction = dir.normalized;
    }

    public override void Exit()
    {
        base.Exit();

        ResetControlVariables();
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
