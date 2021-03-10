using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float PD_maxHitPoint = 100f;
    public float PD_maxStunPoint = 5f;
    public float PD_maxDecayPoint = 20f;

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
    public float MAS_damageAmount = 10f;
    public float MAS_stunAmount = 1f;
    public Vector2 MAS_knockbackDirection = new Vector2(1, 1);
    public float MAS_knockbackImpulse = 4f;
    public float MAS_damageTime = 0.2f;
    public GameObject MAS_meleeAttackParticle;
    public float MAS_attackCooldownTimer = 1f;

    [Header("Parry State")]
    public float PS_maxTime = 1f;
    public float PS_horizontalSpeed = 1f;
    public GameObject PS_particle;
    public float PS_damage = 1f;
    public float PS_coolDownTimer = 1f;

    [Header("Stun State")]
    public float SS_stunDurationTime = 0.3f;

    [Header("Roll State")]
    public float RS_rollSpeed = 4f;
    public float RS_CoolDownTimer = 1f;

    [Header("Converse State")]
    public float CS_selectionTimer = 0.2f;

    [Header("Ladder State")]
    public float LS_climbSpeed = 2f;
    public float LS_jumpOffSpeed = 5f;
}
