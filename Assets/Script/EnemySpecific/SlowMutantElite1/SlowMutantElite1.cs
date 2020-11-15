using Gulch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMutantElite1 : Entity
{
    // ===================== DATA ==========================
    public WalkStateData walkStateData;
    public DeadStateData deadStateData;
    public StunStateData stunStateData;
    public MeleeAttackStateData meleeAttackStateData;
    public DetectPlayerStateData detectPlayerStateData;
    public IdleStateData stageTwoIdleStateData;
    public ChargeStateData chargeStateData;
    public EvadeStateData evadeStateData;

    public MeleeAttackStateData tentacleAttackStateData;
    // ====================== DATA =========================

    public SME1_StoneState stoneState;
    public SME1_RecoverState recoverState;
    public SME1_WalkState walkState;
    public SME1_FlipState flipState;
    public SME1_DeadState deadState;
    public SME1_StunState stunState;
    public SME1_HeideAttackState heideAttackState;
    public SME1_ChargeState chargeState;
    public SME1_EvadeState evadeState;
    public SME1_DetectPlayerState detectPlayerState;
    public SME1_TransformState transformState;

    // Stage Two
    public SME1_StageTwoIdleState stageTwoIdleState;
    public SME1_StageTwoHeideAttackState stageTwoHeideAttackState;
    public SME1_StageTwoFlipState stageTwoFlipState;
    public SME1_StageTwoTentacleAttackState stageTwoTentacleAttackState;

    [SerializeField]
    public bool DrawGizmos = true;

    // bool isFirstTimeAwake;
    public bool isAwake { get; private set; }

    public int currentStage { get; private set; }

    public Transform snakeHeadsParent;

    public List<SME1_SnakeHead> SnakeHeads { get; private set; }

    void Awake()
    {
        InitSnakeHead();
    }

    private void InitSnakeHead()
    {
        SnakeHeads = new List<SME1_SnakeHead>();
        SnakeHeads.Add(snakeHeadsParent.GetChild(0).GetComponent<SME1_SnakeHead>());
        SnakeHeads.Add(snakeHeadsParent.GetChild(1).GetComponent<SME1_SnakeHead>());
        SnakeHeads.Add(snakeHeadsParent.GetChild(2).GetComponent<SME1_SnakeHead>());
        SnakeHeads.Add(snakeHeadsParent.GetChild(3).GetComponent<SME1_SnakeHead>());

        SnakeHeads[0].index = 0;
        SnakeHeads[1].index = 1;
        SnakeHeads[2].index = 2;
        SnakeHeads[3].index = 3;
    }

    protected override void Start()
    {
        base.Start();
        SetInitialFacintDirection(-1);

        stoneState = new SME1_StoneState(stateMachine, this, "stone", this);
        recoverState = new SME1_RecoverState(stateMachine, this, "recover", this);
        walkState = new SME1_WalkState(stateMachine, this, "move", walkStateData, this);
        flipState = new SME1_FlipState(stateMachine, this, "flip", walkState, this);
        deadState = new SME1_DeadState(stateMachine, this, "dead", deadStateData, this);
        stunState = new SME1_StunState(stateMachine, this, "stun", stunStateData, this);
        heideAttackState = new SME1_HeideAttackState(stateMachine, this, "heideAttack", meleeAttackStateData, hitbox, this);
        chargeState = new SME1_ChargeState(stateMachine, this, "charge", meleeAttackStateData, chargeStateData, damageBox, this);
        evadeState = new SME1_EvadeState(stateMachine, this, "evade", evadeStateData, this);
        detectPlayerState = new SME1_DetectPlayerState(stateMachine, this, "empty", detectPlayerStateData, this);
        transformState = new SME1_TransformState(stateMachine, this, "transform", this);

        // ==== STAGE TWO ====
        stageTwoIdleState = new SME1_StageTwoIdleState(stateMachine, this, "s2_idle", stageTwoIdleStateData, this);
        stageTwoHeideAttackState = new SME1_StageTwoHeideAttackState(stateMachine, this, "s2_heideAttack", meleeAttackStateData, hitbox, this);
        stageTwoFlipState = new SME1_StageTwoFlipState(stateMachine, this, "s2_flip", stageTwoIdleState, this);
        stageTwoTentacleAttackState = new SME1_StageTwoTentacleAttackState(stateMachine, this, "s2_idle", tentacleAttackStateData, this);

        // reading data from loaded save database
        isAwake = false;
        // if statement here to check if first time
        stateMachine.Initialize(stoneState);

        stateCooldownTimer = new StateCooldownTimer();
        stateCooldownTimer.AddStateTimer(chargeState);
        stateCooldownTimer.AddStateTimer(heideAttackState);
        stateCooldownTimer.AddStateTimer(evadeState);

        stateMachine.SetStateCooldownTimer(stateCooldownTimer);

        // init state
        currentStage = 0;
    }

    protected override void Damage(CombatData combatData)
    {
        if (!isDanmageable)
            return;

        if (stateMachine.currentState == deadState)
        {
            return;
        }

        base.Damage(combatData);

        if (isDead)
        {
            stateMachine.SwitchState(deadState);
            return;
        }

        switch (currentStage)
        {
            case 0:
                {
                    if (currentHealth <= entityData.maxHealth / 2)
                    {
                        /* ========= Change to Stage Two ========= */
                        stateCooldownTimer.RemoveStateTimer(chargeState);
                        stateCooldownTimer.RemoveStateTimer(heideAttackState);
                        stateCooldownTimer.RemoveStateTimer(evadeState);

                        stateCooldownTimer.AddStateTimer(stageTwoHeideAttackState);
                        stateCooldownTimer.AddStateTimer(stageTwoTentacleAttackState);

                        currentStage = 1;
                        stateMachine.SwitchState(transformState);
                        /* ========= Change to Stage Two ========= */
                    }
                    else if(stateMachine.currentState == stunState)
                    {
                        break;
                    }
                    else if (isStunned || combatData.isParryDamage)
                    {
                        if (combatData.facingDirection == facingDirection)
                        {
                            Flip();
                        }
                        stateMachine.SwitchState(stunState);
                        ResetStunResistance();
                    }
                    else if (stateMachine.currentState != heideAttackState)
                    {
                        bool damageFrom = combatData.position.x - transform.position.x > 0;
                        if (damageFrom != facingDirection > 0)
                        {
                            flipState.SetPrevState(heideAttackState);
                            stateMachine.SwitchState(flipState);
                        }
                        else
                        {
                            stateMachine.SwitchState(heideAttackState);
                        }
                    }
                }
                break;
            case 1:
                break;
            default:
                break;
        }

        GameEventListener.Instance.OnTakeDamage(new TakeDamageData(aliveGO, SpriteEffectType.Blink));
    }

    protected override void KnockBack(CombatData combatData)
    {
        base.KnockBack(combatData);
    }

    [ExecuteInEditMode]
    protected override void OnDrawGizmos()
    {
        if(DrawGizmos)
            base.OnDrawGizmos();

        Gizmos.DrawWireSphere(hitbox.position, meleeAttackStateData.attackRadius);
    }
}
