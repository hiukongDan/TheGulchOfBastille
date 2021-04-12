using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "newPlayerStateMachineData", menuName = "Data/Player/State Machine Data")]
public class D_PlayerStateMachine : ScriptableObject
{
    [Header("General Data")]
    public float GD_groundCheckDistance = 0.3f;
    public float GD_wallCheckDistance = 0.3f;
    public float GD_ladderEndCheckRadius = 0.1f;
    public float GD_damageImmuneTime = 0.5f;
    public LayerMask GD_whatIsGround;
    public LayerMask GD_whatIsPlatform;
    public LayerMask GD_whatIsEnemy;
    public LayerMask GD_whatIsLadder;

    [Header("Player Data")]
    public int GD_playerLevel = 1; // initial level is 1
    public void Levelup(){
        if(ItemData.playerLevelUpData.Length > GD_playerLevel){
            GD_playerLevel++;
        }
    }
    public float PD_maxHitPoint{
        get{
            return ItemData.playerLevelUpData[GD_playerLevel].HP;
        }
    }
    public float PD_maxStunPoint{
        get{
            return ItemData.playerLevelUpData[GD_playerLevel].StunP;
        }
    }
    public float PD_maxDecayPoint{
        get{
            return ItemData.playerLevelUpData[GD_playerLevel].DP;
        }
    }

    [Header("Ground State")]
    public float GS_coyoteTime = 0.2f;


    [Header("Walk State")]
    public float WS_walkSpeed = 4f;

    [Header("InAir State")]
    public int IAS_jumpAmounts = 1;
    public float IAS_jumpTryTime = 0.2f;

    [Header("Dash State")]
    public int DS_dashAmounts = 1;
    public float DS_dashTime = 0.11f;
    public float DS_dashSpeed = 15f;
    public float DS_dashCoolDownTime = 1.4f;

    [Header("Wall State")]
    public int WS_wallJumpAmounts = 1;
    public Vector2 WS_wallJumpImpulse = new Vector2(4, 11);
    public float WS_wallJumpHoldTime = 0.2f;


    [Header("Jump State")]
    public float JS_jumpSpeed = 11f;
    public float JS_horizontalSpeed = 4f;
    public float JS_jumpCanceledMultiplier = 0.5f;

    [Header("MeleeAttack State")]
    public float MAS_hitboxRadius = 0.5f;
    public float MAS_damageAmount{
        get{
            return ItemData.playerLevelUpData[GD_playerLevel].AP;
        }
    }
    public float MAS_stunAmount = 1f;
    public Vector2 MAS_knockbackDirection = new Vector2(1, 1);
    public float MAS_knockbackImpulse = 4f;
    public float MAS_damageTime = 0.2f;
    public GameObject MAS_meleeAttackParticle;
    public float MAS_attackCooldownTimer = 1f;

    [Header("Take Damage State")]
    public float TDS_takeDamageDuration = 0.1f;

    [Header("Parry State")]
    public float PS_maxTime = 1f;
    public float PS_horizontalSpeed = 1f;
    public GameObject PS_particle;
    public float PS_damage = 1f;
    public float PS_coolDownTimer = 1f;

    [Header("Stun State")]
    public float SS_stunDurationTime = 0.3f;

    [Header("Roll State")]
    public float RS_rollSpeed = 3f;
    public float RS_CoolDownTimer = 1f;

    [Header("Converse State")]
    public float CS_selectionTimer = 0.2f;

    [Header("Ladder State")]
    public float LS_climbSpeed = 2f;
    public float LS_jumpOffSpeed = 5f;

    [Serializable]
    public struct PlayerSaveData{
        public int GD_playerLevel;
        public float MAS_stunAmount;
        public float PS_damage;

        public PlayerSaveData(int GD_playerLevel, float MAS_stunAmount, float PS_damage){
            this.GD_playerLevel = GD_playerLevel;
            this.MAS_stunAmount = MAS_stunAmount;
            this.PS_damage = PS_damage;
        }
    };

    public void SetPlayerSaveData(PlayerSaveData playerSaveData){
            this.GD_playerLevel = playerSaveData.GD_playerLevel;
            this.MAS_stunAmount = playerSaveData.MAS_stunAmount;
            this.PS_damage = playerSaveData.PS_damage;
    }

    public PlayerSaveData GetPlayerSaveData(){
        return new PlayerSaveData(GD_playerLevel, MAS_stunAmount, PS_damage);
    }
}
