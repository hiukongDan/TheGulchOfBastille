using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected FiniteStateMachine stateMachine;
    protected StateCooldownTimer stateCooldownTimer;

    public int facingDirection { get; private set; }
    public enum FacingDirection{
        LEFT, RIGHT,
    }
    public FacingDirection startFacingDirection;
    public Transform
        wallCheck,
        edgeCheck,
        groundCheck,
        hitbox,
        damageBox,
        meleeAttackCheck,
        detectCenter,
        initPosition;

    public Vector2 DamageBoxSize
    {
        get
        {
            if(objectToAlive != null)
            {
                return objectToAlive.DamageBoxSize;
            }
            else if(entityData != null)
            {
                return new Vector2(entityData.damageBoxWidth, entityData.damageBoxHeight);
            }
            return Vector2.zero;
        }
    }

    public EntityData entityData;

    public GameObject aliveGO;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public ObjectToAlive objectToAlive;
    public EntityEventHandler entityEventHandler;

    protected CombatData combatData;

    protected float currentHealth;
    protected float currentStunResistance;

    protected bool isDead;
    protected bool isStunned;

    protected Vector2 vectorWorkspace = new Vector2();

    public bool isDanmageable { get; private set; }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;

        anim = aliveGO.GetComponent<Animator>();
        rb = aliveGO.GetComponent<Rigidbody2D>();

        stateMachine = new FiniteStateMachine(this);

        objectToAlive = aliveGO.GetComponent<ObjectToAlive>();

        facingDirection = 1;

        Reset();
    }

    protected virtual void Reset(){
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        isDead = false;
        isStunned = false;
        isDanmageable = true;

        switch(startFacingDirection){
            case FacingDirection.LEFT:
                if(facingDirection == 1){
                    Flip();
                }
                break;
            case FacingDirection.RIGHT:
                if(facingDirection == -1){
                    Flip();
                }
                break;
            default:
            break;
        }
    }

    protected virtual void OnEnable() {
        InitEntity();
    }

    public virtual void InitEntity()
    {
        if (!GetComponent<EnemySaveData>().IsAlive())
        {
            aliveGO.SetActive(false);
        }
        else{
            aliveGO.SetActive(true);
            Reset();
        }

        aliveGO.transform.position = initPosition.transform.position;
    }

    protected virtual void Update()
    {
        stateMachine.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public bool DetectEdge()
    {
        return Physics2D.Raycast(edgeCheck.position, Vector2.down, entityData.edgeCheckDistance, entityData.whatIsGround | entityData.whatIsPlatform).collider == null;
    }

    public bool DetectWall()
    {
        return Physics2D.Raycast(wallCheck.position, new Vector2(facingDirection, 0), entityData.wallCheckDistance, entityData.whatIsGround | entityData.whatIsPlatform);
    }

    public bool DetectGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckDistance, entityData.whatIsGround);
    }

    public bool DetectPlatform()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckDistance, entityData.whatIsPlatform);
    }

    public bool DetectPlayer()
    {
        return Physics2D.Raycast(meleeAttackCheck.position, new Vector2(facingDirection, 0), entityData.meleeAttackDistance, entityData.whatIsPlayer);
    }

    public virtual void Flip()
    {
        float before = aliveGO.transform.rotation.y;
        aliveGO.transform.Rotate(new Vector3(0, 180, 0));
        float after = aliveGO.transform.rotation.y;
        Debug.Log("Before: " + before + " After: " + after);
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

    // RETURN: true if actually flipped
    protected virtual bool FaceTo(Vector2 targetPos)
    {
        if (targetPos.x - aliveGO.transform.position.x > 0 != facingDirection > 0)
        {
            Flip();
            return true;
        }
        return false;
    }

    protected virtual void Damage(CombatData combatData)
    {
        if (isDead)
            return;

        combatData.from?.gameObject.SendMessage("ValidAttack");
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

    protected void SetInitialFacingDirection(int newDirection) => facingDirection = newDirection;
    public bool SetIsDamageable(bool newIsDamageable)
    {
        isDanmageable = newIsDamageable;
        return isDanmageable;
    }

    public bool IsDead() => isDead;

    protected virtual void OnDead(){
        UilosGroup uilosGroup = GetComponent<UilosGroup>();
        if(uilosGroup){
            uilosGroup.OnGenerateUilos(aliveGO.transform.position);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + new Vector3(entityData.wallCheckDistance * facingDirection, 0));
        Gizmos.DrawLine(edgeCheck.position, edgeCheck.position + Vector3.down * entityData.edgeCheckDistance);
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * entityData.groundCheckDistance);

        Gizmos.DrawLine(aliveGO.transform.position, aliveGO.transform.position + new Vector3(entityData.meleeAttackDistance, 0));
        //var pos = detectCenter ? detectCenter.position : aliveGO.transform.position;
        //Gizmos.DrawWireCube(new Vector3(pos.x + entityData.meleeAttackDistance / 2, pos.y), new Vector3(entityData.meleeAttackDistance, entityData.meleeAttackDistance));

        Gizmos.DrawWireSphere(aliveGO.transform.position + Vector3.right * entityData.detectPlayerAgroMaxDistance, 0.2f);
        Gizmos.DrawWireSphere(aliveGO.transform.position + Vector3.right * entityData.detectPlayerAgroMinDistance, 0.2f);

        Gizmos.DrawLine(new Vector2(damageBox.position.x - DamageBoxSize.x / 2, damageBox.position.y - DamageBoxSize.y / 2),
            new Vector2(damageBox.position.x + DamageBoxSize.x / 2, damageBox.position.y - DamageBoxSize.y / 2));
        Gizmos.DrawLine(new Vector2(damageBox.position.x + DamageBoxSize.x / 2, damageBox.position.y - DamageBoxSize.y / 2),
            new Vector2(damageBox.position.x + DamageBoxSize.x / 2, damageBox.position.y + DamageBoxSize.y / 2));
        Gizmos.DrawLine(new Vector2(damageBox.position.x + DamageBoxSize.x / 2, damageBox.position.y + DamageBoxSize.y / 2),
            new Vector2(damageBox.position.x - DamageBoxSize.x / 2, damageBox.position.y + DamageBoxSize.y / 2));
        Gizmos.DrawLine(new Vector2(damageBox.position.x - DamageBoxSize.x / 2, damageBox.position.y + DamageBoxSize.y / 2),
            new Vector2(damageBox.position.x - DamageBoxSize.x  / 2, damageBox.position.y - DamageBoxSize.y / 2));
    }
}
