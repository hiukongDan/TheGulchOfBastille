using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerWalkState : PlayerGroundState
{
    protected bool isNeonPotion = false;
    public PlayerWalkState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }
    protected override void DoCheck()
    {
        base.DoCheck();
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

        if (shouldFlip && canFlip)
        {
            player.Flip();
        }

        if (isMeleeAttack || isRoll || isParry)
        {
            player.SetVelocityX(0f);
        }

        if (!isAction)
        {
            if (isGrounded && normMovementInput.x == 0)
            {
                stateMachine.SwitchState(player.idleState);
            }
            else if (isGrounded && normMovementInput.x != 0)
            {
                workspace.Set(normMovementInput.x * data.WS_walkSpeed, 0f);
                if(isNeonPotion){
                    workspace *= ItemData.ConsumableItemData.Neon_Potion_velocityMultiplier;
                }
                player.SetVelocity(workspace);
            }
            else if (!isGrounded)
            {
                stateMachine.SwitchState(player.inAirState);
            }
        }
    }

    public void SetNeonPotion(bool value) => isNeonPotion = value;

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SetAnimationCodeFromWeapon(){
        switch(player.playerRuntimeData.GetCurrentWeaponInfo().weapon){
            case ItemData.Weapon.Iron_Sword:
                SetAnimationCode(AlfAnimationHash.RUN_IRONSWORD);
                break;
            case ItemData.Weapon.Claymore:
                SetAnimationCode(AlfAnimationHash.RUN_CLAYMORE);
                break;
            case ItemData.Weapon.Dragon_Slayer_Sword:
                SetAnimationCode(AlfAnimationHash.RUN_DRAGONSLAYER);
                break;
            case ItemData.Weapon.Wood_Bow:
                SetAnimationCode(AlfAnimationHash.RUN_WOODBOW);
                break;
            case ItemData.Weapon.Elf_Bow:
                SetAnimationCode(AlfAnimationHash.RUN_ELFBOW);
                break;
            case ItemData.Weapon.Long_Bow:
                SetAnimationCode(AlfAnimationHash.RUN_LONGBOW);
                break;
            case ItemData.Weapon.Apprentice_Stick:
                SetAnimationCode(AlfAnimationHash.RUN_APPRENTICE_STICK);
                break;
            case ItemData.Weapon.Master_Stick:
                SetAnimationCode(AlfAnimationHash.RUN_MASTER_STICK);
                break;
            case ItemData.Weapon.Sunlight_Stick:
                SetAnimationCode(AlfAnimationHash.RUN_SUNLIGHT_STICK);
                break;
            default:
                SetAnimationCode(AlfAnimationHash.RUN_0);
                break;
        }
    }
}
