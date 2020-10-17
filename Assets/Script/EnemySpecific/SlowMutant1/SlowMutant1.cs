using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SlowMutant1 : Entity
{
    public IdleStateData idleStateData;
    public WalkStateData walkStateData;
    public MeleeAttackStateData meleeAttackStateData;
    public DetectPlayerStateData detectPlayerStateData;
    public FleeStateData fleeStateData;
    public DeadStateData deadStateData;
    public InAirStateData inAirStateData;
    public StunStateData stunStateData;
    public TakeDamageStateData takeDamageStateData;

    public SM1_IdleState idleState { get; private set; }
    public SM1_WalkState walkState { get; private set; }
    public SM1_DetectPlayerState detectPlayerState { get; private set; }
    public SM1_MeleeAttackState meleeAttackState { get; private set; }
    public SM1_FleeState fleeState { get; private set; }
    public SM1_DeadState deadState { get; private set; }
    public SM1_InAirState inAirState { get; private set; }
    public SM1_StunState stunState { get; private set; }
    public SM1_TakeDamageState takeDamageState { get; private set; }

    public Transform hitbox;

    public float idleStayTimeInLookup;
    public float idleStayTimeInLookfront;
    public Vector2 changeIdleAnimationTimeRange;

    protected override void Start()
    {
        base.Start();
        idleState = new SM1_IdleState(stateMachine, this, "idle", idleStateData, this, idleStayTimeInLookup, idleStayTimeInLookfront, changeIdleAnimationTimeRange);
        walkState = new SM1_WalkState(stateMachine, this, "walk", walkStateData, this);
        detectPlayerState = new SM1_DetectPlayerState(stateMachine, this, "detectPlayer", detectPlayerStateData, this);
        meleeAttackState = new SM1_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackStateData, hitbox, this);
        fleeState = new SM1_FleeState(stateMachine, this, "flee", fleeStateData, this);
        deadState = new SM1_DeadState(stateMachine, this, "dead", deadStateData, this);
        stunState = new SM1_StunState(stateMachine, this, "stun", stunStateData, this);
        inAirState = new SM1_InAirState(stateMachine, this, "inAir", inAirStateData, this);
        takeDamageState = new SM1_TakeDamageState(stateMachine, this, "takeDamage", takeDamageStateData, this);

        stateMachine.Initialize(walkState);
    }

    private void Update()
    {
        stateMachine.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    protected override void Damage(CombatData combatData)
    {
        base.Damage(combatData);

        if (isDead)
        {
            if(stateMachine.currentState != deadState)
            {
                stateMachine.SwitchState(deadState);
            }
        }
        else if (isStunned)
        {
            knockback(rb, combatData.position.x, aliveGO.transform.position.x, combatData.knockbackDir, combatData.knockbackImpulse);
            stateMachine.SwitchState(stunState);
        }
        else
        {
            stateMachine.SwitchState(takeDamageState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(hitbox.position, meleeAttackStateData.attackRadius);
    }

}
