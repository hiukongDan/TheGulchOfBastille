using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerStateMachineData", menuName = "Data/Player/State Machine Data")]
public class D_PlayerStateMachine : ScriptableObject
{
    //[Header("Idle State")]

    [Header("General Data")]
    public float GD_groundCheckDistance = 0.3f;
    public LayerMask GD_whatIsGround;
    public LayerMask GD_whatIsPlatform;
    public LayerMask GD_whatIsEnemy;

    [Header("Player Data")]
    public float PD_maxHitpoint = 100f;
    public float PD_maxStunPoint = 5f;

    [Header("Ground State")]
    public float GS_coyoteTime = 0.2f;


    [Header("Walk State")]
    public float WS_walkSpeed = 4f;

    [Header("InAir State")]
    public int IAS_jumpAmounts = 1;
    public float IAS_jumpTryTime = 0.2f;


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

    [Header("Parry State")]
    public float PS_maxTime = 1f;
    public float PS_horizontalSpeed = 1f;
}
