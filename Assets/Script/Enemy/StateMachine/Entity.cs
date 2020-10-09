using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected FiniteStateMachine stateMachine;

    public int facingDirection { get; private set; }

    public Transform
        wallCheck,
        edgeCheck,
        groundCheck,
        damageBox,
        meleeAttackCheck;

    public EntityData entityData;

    public GameObject aliveGO;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public ObjectToAlive objectToAlive;

    protected CombatData combatData;

    protected float currentHealth;
    protected float currentStunResistance;

    protected bool isDead;
    protected bool isStunned;

    protected Vector2 vectorWorkspace = new Vector2();

    protected virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;

        anim = aliveGO.GetComponent<Animator>();
        rb = aliveGO.GetComponent<Rigidbody2D>();

        stateMachine = new FiniteStateMachine(this);

        facingDirection = 1;

        objectToAlive = aliveGO.GetComponent<ObjectToAlive>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        isDead = false;
        isStunned = false;
    }

    public bool DetectEdge()
    {
        return Physics2D.Raycast(edgeCheck.position, Vector2.down, entityData.edgeCheckDistance, entityData.whatIsGround).collider == null;
    }

    public bool DetectWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public bool DetectGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckDistance, entityData.whatIsGround);
    }

    public bool DetectPlayer()
    {
        return Physics2D.Raycast(meleeAttackCheck.position, aliveGO.transform.right, entityData.meleeAttackDistance, entityData.whatIsPlayer);
    }

    public void Flip()
    {
        aliveGO.transform.Rotate(new Vector3(0, 180, 0));
        facingDirection *= -1;
    }

    protected void CheckDamageBox()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(damageBox.position, new Vector2(entityData.damageBoxWidth, entityData.damageBoxHeight), entityData.whatIsPlayer);
        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                combatData.damage = entityData.damage;
                combatData.position = aliveGO.transform.position;
                combatData.stunDamage = entityData.stunDamage;
                combatData.knockbackDir = entityData.knockbackDir.normalized;
                combatData.knockbackImpulse = entityData.knockbackImpulse;
                combatData.from = gameObject;
                collider.gameObject.SendMessage("Damage", combatData);
            }
        }
    }

    protected virtual void Damage(CombatData combatData)
    {
        currentHealth -= combatData.damage;

        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
        }
        else if(!isStunned && !isDead)
        {
            currentStunResistance -= combatData.stunDamage;
            if(currentStunResistance <= 0)
            {
                isStunned = true;
                ResetStunResistance();
            }
        }
    }

    protected virtual void KnockBack(CombatData combatData)
    {

    }

    public void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public void knockback(Rigidbody2D rb, float fromX, float thisX, Vector2 impulseDir, float impulse)
    {
        int dir = (fromX > thisX ? -1 : 1);
        impulseDir.Normalize();
        vectorWorkspace.Set(impulseDir.x * dir * impulse, impulseDir.y * impulse);
        rb.velocity = vectorWorkspace;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + new Vector3(entityData.wallCheckDistance * facingDirection, 0));
        Gizmos.DrawLine(edgeCheck.position, edgeCheck.position + Vector3.down * entityData.edgeCheckDistance);
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * entityData.groundCheckDistance);

        Gizmos.DrawLine(aliveGO.transform.position, aliveGO.transform.position + new Vector3(entityData.meleeAttackDistance, 0));

        Gizmos.DrawWireSphere(aliveGO.transform.position + Vector3.right * entityData.detectPlayerAgroMaxDistance, 0.2f);
        Gizmos.DrawWireSphere(aliveGO.transform.position + Vector3.right * entityData.detectPlayerAgroMinDistance, 0.2f);

        Gizmos.DrawLine(new Vector2(damageBox.position.x - entityData.damageBoxWidth / 2, damageBox.position.y - entityData.damageBoxHeight / 2),
            new Vector2(damageBox.position.x + entityData.damageBoxWidth / 2, damageBox.position.y - entityData.damageBoxHeight / 2));
        Gizmos.DrawLine(new Vector2(damageBox.position.x + entityData.damageBoxWidth / 2, damageBox.position.y - entityData.damageBoxHeight / 2),
            new Vector2(damageBox.position.x + entityData.damageBoxWidth / 2, damageBox.position.y + entityData.damageBoxHeight / 2));
        Gizmos.DrawLine(new Vector2(damageBox.position.x + entityData.damageBoxWidth / 2, damageBox.position.y + entityData.damageBoxHeight / 2),
            new Vector2(damageBox.position.x - entityData.damageBoxWidth / 2, damageBox.position.y + entityData.damageBoxHeight / 2));
        Gizmos.DrawLine(new Vector2(damageBox.position.x - entityData.damageBoxWidth / 2, damageBox.position.y + entityData.damageBoxHeight / 2),
            new Vector2(damageBox.position.x - entityData.damageBoxWidth / 2, damageBox.position.y - entityData.damageBoxHeight / 2));
    }
}
