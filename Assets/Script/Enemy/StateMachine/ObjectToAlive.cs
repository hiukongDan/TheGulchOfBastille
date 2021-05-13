using UnityEngine;

public class ObjectToAlive : MonoBehaviour
{
    public MeleeAttackState meleeAttackState;
    public StunState stunState;
    public TakeDamageState takeDamageState;
    public FlipState flipState;
    public EvadeState evadeState;

    private Entity entity;

    public Vector2 DamageBoxSize;

    void Start()
    {
        entity = transform.parent.GetComponent<Entity>();
    }

    public void Damage(CombatData combatData)
    {
        entity.SendMessage("Damage", combatData);
    }

    public void CompleteFlip()
    {
        flipState?.CompleteFlip();
    }

    public virtual void Flip()
    {
        entity.Flip();
    }

    public void DoMeleeAttack()
    {
        if(meleeAttackState != null)
            meleeAttackState.DoMeleeAttack();
    }

    public void CompleteMeleeAttack()
    {
        if(meleeAttackState != null)
            meleeAttackState.CompleteMeleeAttack();
    }

    public void CompleteStun()
    {
        if (stunState != null)
            stunState.CompleteStun();
    }

    public void CompleteTakeDamage()
    {
        if(takeDamageState != null)
        {
            takeDamageState.CompleteTakeDamage();
        }
    }

    public void CompleteEvade()
    {
        evadeState?.CompleteEvade();
    }
}
