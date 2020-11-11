using Gulch;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMutantElite1 : Entity
{
    public WalkStateData walkStateData;
    public DeadStateData deadStateData;
    public StunStateData stunStateData;
    public MeleeAttackStateData meleeAttackStateData;
    public DetectPlayerStateData detectPlayerStateData;

    public SME1_StoneState stoneState;
    public SME1_RecoverState recoverState;
    public SME1_WalkState walkState;
    public SME1_FlipState flipState;
    public SME1_DeadState deadState;
    public SME1_StunState stunState;
    public SME1_HeideAttackState heideAttackState;
    public SME1_EvadeState evadeState;
    public SME1_DetectPlayerState detectPlayerState;
    public SME1_TransformState transformState;

    // temp
    public SME1_StageTwoIdleState stageTwoIdleState;

    [SerializeField]
    public bool DrawGizmos = true;

    // bool isFirstTimeAwake;
    public bool isAwake { get; private set; }

    public int stageNum = 0;

    public Transform snakeHeads;
    public Animator SH_CharlieAnim { get; private set; }
    public Animator SH_OscarAnim { get; private set; }
    public Animator SH_BaptesteAnim { get; private set; }
    public Animator SH_SombreAnim { get; private set; }

    void Awake()
    {
        if(snakeHeads != null)
        {
            SH_CharlieAnim = snakeHeads.GetChild(0).GetComponent<Animator>();
            SH_OscarAnim = snakeHeads.GetChild(1).GetComponent<Animator>(); ;
            SH_BaptesteAnim = snakeHeads.GetChild(2).GetComponent<Animator>(); ;
            SH_SombreAnim = snakeHeads.GetChild(3).GetComponent<Animator>(); ;
        }
    }

    protected override void Start()
    {
        base.Start();
        SetInitialFacintDirection(-1);

        stoneState = new SME1_StoneState(stateMachine, this, "stone", this);
        recoverState = new SME1_RecoverState(stateMachine, this, "recover", this);
        walkState = new SME1_WalkState(stateMachine, this, "move", walkStateData, this);
        flipState = new SME1_FlipState(stateMachine, this, "flip", this);
        deadState = new SME1_DeadState(stateMachine, this, "dead", deadStateData, this);
        stunState = new SME1_StunState(stateMachine, this, "stun", stunStateData, this);
        heideAttackState = new SME1_HeideAttackState(stateMachine, this, "heideAttack", meleeAttackStateData, hitbox, this);
        evadeState = new SME1_EvadeState(stateMachine, this, "evade", this);
        detectPlayerState = new SME1_DetectPlayerState(stateMachine, this, null, detectPlayerStateData, this);
        transformState = new SME1_TransformState(stateMachine, this, "transform", this);
        stageTwoIdleState = new SME1_StageTwoIdleState(stateMachine, this, "stageTwoIdle", this);

        // reading data from loaded save database
        isAwake = false;
        // if statement here to check if first time
        stateMachine.Initialize(stoneState);

        // init state
        stageNum = 0;
    }
    protected override void Damage(CombatData combatData)
    {
        if (!isDanmageable)
            return;

        base.Damage(combatData);

        if (stateMachine.currentState == deadState)
        {
            return;
        }
        else if (isDead)
        {
            stateMachine.SwitchState(deadState);
            return;
        }

        switch (stageNum)
        {
            case 0:
                {
                    if (currentHealth < entityData.maxHealth / 2)
                    {
                        stageNum = 1;
                        stateMachine.SwitchState(transformState);
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
