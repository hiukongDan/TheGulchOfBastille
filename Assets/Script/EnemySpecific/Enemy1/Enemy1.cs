using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy1 : Entity
{
    public IdleStateData idleStateData;
    public WalkStateData walkStateData;
    public ChargeStateData chargetStateData;
    public LookForPlayerStateData lookForPlayerStateData;
    public MeleeAttackStateData meleeAttackStateData;
    public StunStateData stunStateData;
    public DeadStateData deadStateData;
    public DetectPlayerStateData detectPlayerStateData;

    public E1_IdleState idleState { get; private set; }
    public E1_WalkState walkState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_DetectPlayerState detectPlayerState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_StunState stunState { get; private set; }
    public E1_DeadState deadState { get; private set; }

    public Transform hitBoxPoint;

    protected override void Start()
    {
        base.Start();
        idleState = new E1_IdleState(stateMachine, this, "idle", idleStateData, this);
        walkState = new E1_WalkState(stateMachine, this, "walk", walkStateData, this);
        chargeState = new E1_ChargeState(stateMachine, this, "charge", chargetStateData, this);
        detectPlayerState = new E1_DetectPlayerState(stateMachine, this, "detectPlayer", detectPlayerStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E1_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackStateData, hitBoxPoint, this);
        stunState = new E1_StunState(stateMachine, this, "stun", stunStateData, this);
        deadState = new E1_DeadState(stateMachine, this, "dead", deadStateData, this);

        stateMachine.Initialize(walkState);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        CheckDamageBox();
    }

    protected override void Damage(CombatData combatData)
    {
        base.Damage(combatData);
        if (isDead)
        {
            stateMachine.SwitchState(deadState);
        }
        else if (isStunned)
        {
            stateMachine.SwitchState(stunState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(hitBoxPoint.position, meleeAttackStateData.attackRadius);
    }
}
