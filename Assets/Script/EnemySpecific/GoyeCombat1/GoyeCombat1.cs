using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoyeCombat1 : Entity
{
    #region REFERENCES
    public Player refPlayer {get; private set; }
    public GC1_ObjectToAlive gc1_ota { get; private set; }
    #endregion

    #region STATE
    public GC1_BattleBeginState battleBeginState;
    public GC1_ChargeState chargeState;
    public GC1_DefeatState defeatState;
    public GC1_DefenceState defenceState;
    public GC1_DetectPlayerState detectPlayerState;
    public GC1_EvadeState evadeState;
    public GC1_CombatIdleState combatIdleState;
    public GC1_ParryState parryState;
    public GC1_RunState runState;
    public GC1_StunState stunState;
    public GC1_MeleeAttackState meleeAttackState;
    // used for flip times limitation
    private GC1_FlipState flipState;
    #endregion

    #region STATE_DATA
    public DefenceStateData defenceStateData;
    public DetectPlayerStateData detectPlayerStateData;
    public EvadeStateData evadeStateData;
    public IdleStateData idleStateData;
    public WalkStateData runStateData;
    public StunStateData stunStateData;
    public ParryStateData parryStateData;
    public MeleeAttackStateData meleeAttackStateData;
    public MeleeAttackStateData chargeStateData;
    #endregion

    protected override void Damage(CombatData combatData)
    {
        if (!isDanmageable)
        {
            return;
        }

        if(stateMachine.currentState == defeatState)
        {
            return;
        }

        if(stateMachine.currentState == defenceState && defenceState.isCounterAttack)
        {
            return;
        }

        base.Damage(combatData);

        if(isDead)
        {
            stateMachine.SwitchState(defeatState);
        }
        else if(stateMachine.currentState == stunState)
        {
            return;
        }
        else if (isStunned)
        {
            if(facingDirection == combatData.facingDirection)
            {
                Flip();
            }

            stateMachine.SwitchState(stunState);
            ResetStunResistance();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void KnockBack(CombatData combatData)
    {
        base.KnockBack(combatData);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(hitbox.position, meleeAttackStateData.attackRadius);
    }

    protected override void Awake()
    {
        base.Awake();
        /* --------- ASIGN REFERENCEs HERE --------------*/
        refPlayer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();
        gc1_ota = (GC1_ObjectToAlive)objectToAlive;

        battleBeginState = new GC1_BattleBeginState(stateMachine, this, "battleBegin", this);
        chargeState = new GC1_ChargeState(stateMachine, this, "charge", chargeStateData, damageBox,  this);
        defeatState = new GC1_DefeatState(stateMachine, this, "defeat", this);
        defenceState = new GC1_DefenceState(stateMachine, this, "defence", defenceStateData, this);
        detectPlayerState = new GC1_DetectPlayerState(stateMachine, this, null, detectPlayerStateData, this);
        evadeState = new GC1_EvadeState(stateMachine, this, "evade", evadeStateData, this);
        combatIdleState = new GC1_CombatIdleState(stateMachine, this, "combatIdle", idleStateData, this);
        parryState = new GC1_ParryState(stateMachine, this, "parry", parryStateData, this);
        runState = new GC1_RunState(stateMachine, this, "run", runStateData, this);
        stunState = new GC1_StunState(stateMachine, this, "stun", stunStateData, this);
        meleeAttackState = new GC1_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackStateData, hitbox, this);
        flipState = new GC1_FlipState(stateMachine, this, null);

        stateMachine.Initialize(battleBeginState);

        stateCooldownTimer = new StateCooldownTimer();
        stateCooldownTimer.AddStateTimer(meleeAttackState);
        stateCooldownTimer.AddStateTimer(evadeState);
        stateCooldownTimer.AddStateTimer(chargeState);
        stateCooldownTimer.AddStateTimer(defenceState);
        stateCooldownTimer.AddStateTimer(parryState);
        stateCooldownTimer.AddStateTimer(flipState);

        stateMachine.SetStateCooldownTimer(stateCooldownTimer);

        // set rigidbody to static
        gc1_ota.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    protected void OnEnable(){
        Gulch.GameEventListener.Instance.OnPlayerDeadHandler += OnPlayerDead;

        if(!GetComponent<EnemySaveData>().IsAlive()){
            Destroy(gameObject);
        }
    }

    protected void OnDisable() {
        Gulch.GameEventListener.Instance.OnPlayerDeadHandler -= OnPlayerDead;
    }

    protected void OnPlayerDead(){
        // Restore goye combat
        Reset();
        stateMachine.stateCooldownTimer.ResetTimer();
        gc1_ota.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Vector3 initPos = transform.Find("Combat Field/Init Position").position;
        transform.position = initPos;
        stateMachine.Initialize(battleBeginState);
    }

    protected override void Update()
    {
        base.Update();
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>> MESSAGE <<<<<<<<<<<<<<<<<<<<<<<<<<<
    public void CombatTriggered()
    {
        StartCoroutine(battleBeginState.BattleBegin());
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>> MESSAGE <<<<<<<<<<<<<<<<<<<<<<<<<<<


    // >>>>>>>>>>>>>>>>>>>>>>>>>>>  HELPER <<<<<<<<<<<<<<<<<<<<<<<<<<<

    public bool IsPlayerWithinMeleeAttackRange() => Mathf.Abs(refPlayer.transform.position.x - aliveGO.transform.position.x) < entityData.meleeAttackDistance;

    public void FaceToPlayer(bool immediate = true)
    {
        if (immediate || flipState.CanAction())
        {
            if (FaceTo(refPlayer.transform.position))
            {
                flipState.ResetTimer();
            }
        }
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>>  HELPER <<<<<<<<<<<<<<<<<<<<<<<<<<<
}
