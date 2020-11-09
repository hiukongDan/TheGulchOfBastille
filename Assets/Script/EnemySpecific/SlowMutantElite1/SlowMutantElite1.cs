using Gulch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMutantElite1 : Entity
{
    public WalkStateData walkStateData;
    public DeadStateData deadStateData;
    public StunStateData stunStateData;
    public MeleeAttackStateData meleeAttackStateData;

    public SME1_StoneState stoneState;
    public SME1_RecoverState recoverState;
    public SME1_WalkState walkState;
    public SME1_FlipState flipState;
    public SME1_DeadState deadState;
    public SME1_StunState stunState;
    public SME1_HeideAttackState heideAttackState;

    [SerializeField]
    public bool DrawGizmos = true;

    // bool isFirstTimeAwake;
    public bool isAwake { get; private set; }

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

        // reading data from loaded save database
        isAwake = false;
        // if statement here to check if first time
        stateMachine.Initialize(stoneState);
    }
    protected override void Damage(CombatData combatData)
    {
        base.Damage(combatData);

        if (stateMachine.currentState == deadState)
        {
            return;
        }
        else if (isDead)
        {
            stateMachine.SwitchState(deadState);
        }
        // if is in stunState
        else if(isStunned || combatData.isParryDamage)
        {
            if(combatData.facingDirection == facingDirection)
            {
                Flip();
            }
            stateMachine.SwitchState(stunState);
            ResetStunResistance();
        }
        else
        {
            //stateMachine.SwitchState(takeDamageState);
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
