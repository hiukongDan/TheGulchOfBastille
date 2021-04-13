using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, Player player, int animCode, D_PlayerStateMachine data) : base(stateMachine, player, animCode, data)
    {

    }

    protected override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // -- LOGIC BLOCK --------------------------------------------------------
        if (shouldFlip && canFlip)
        {
            player.Flip();
        }

        if (!isAction)
        {
            if (isJump)
            {
                player.stateMachine.SwitchState(player.jumpState);
            }
            else if (normMovementInput.x != 0)
            {
                if(animCode != AlfAnimationHash.IDLE_0)
                {
                    player.walkState.SetAnimationCodeFromWeapon();
                }
                stateMachine.SwitchState(player.walkState);
            }
/*            else if (Mathf.Abs(currentVelocity.y) > 0.01f)
            {
                stateMachine.SwitchState(player.inAirState);
            }*/
        }

        player.SetVelocityX(0);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SetAnimationCodeFromWeapon(){
        switch(player.playerRuntimeData.GetCurrentWeaponInfo().weapon){
            case ItemData.Weapon.Iron_Sword:
                SetAnimationCode(AlfAnimationHash.IDLE_IRONSWORD);
                break;
            case ItemData.Weapon.Claymore:
                SetAnimationCode(AlfAnimationHash.IDLE_CLAYMORE);
                break;
            case ItemData.Weapon.Dragon_Slayer_Sword:
                SetAnimationCode(AlfAnimationHash.IDLE_DRAGONSLAYER);
                break;
            case ItemData.Weapon.Wood_Bow:
                SetAnimationCode(AlfAnimationHash.IDLE_WOODBOW);
                break;
            case ItemData.Weapon.Elf_Bow:
                SetAnimationCode(AlfAnimationHash.IDLE_ELFBOW);
                break;
            case ItemData.Weapon.Long_Bow:
                SetAnimationCode(AlfAnimationHash.IDLE_LONGBOW);
                break;
            case ItemData.Weapon.Apprentice_Stick:
                SetAnimationCode(AlfAnimationHash.IDLE_APPRENTICE_STICK);
                break;
            case ItemData.Weapon.Master_Stick:
                SetAnimationCode(AlfAnimationHash.IDLE_MASTER_STICK);
                break;
            case ItemData.Weapon.Sunlight_Stick:
                SetAnimationCode(AlfAnimationHash.IDLE_MASTER_STICK);
                break;
            default:
                SetAnimationCode(AlfAnimationHash.IDLE_IRONSWORD);
                break;
        }
    }
}
