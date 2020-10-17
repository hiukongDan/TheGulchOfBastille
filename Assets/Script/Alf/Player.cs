using UnityEngine;

public class Player : MonoBehaviour
{
    /* TODO:
     * replace sphere ground check with something more creditable
     */

    #region REFERENCES
    private GameManager GM;
    #endregion

    #region MISC VARIABLES
    public float pixelsPerUnits = 32;
    #endregion

    #region STATEMACHINE
    public PlayerStateMachine stateMachine;

    public PlayerGroundState groundState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerWalkState walkState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerMeleeAttackState meleeAttackState { get; private set; }
    public PlayerParryState parryState { get; private set; }
    public PlayerRollState rollState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    public PlayerWallState wallState { get; private set; }
    public PlayerDashState dashState { get; private set; }

    // TODO: REMOVE STUN STATE
    public PlayerStunState stunState { get; private set; }
    public PlayerTakeDamageState takeDamageState { get; private set; }



    public D_PlayerStateMachine playerData;
    #endregion

    #region ATTACHED STATES
    public PlayerAttackState attackState;
    #endregion

    #region COMPONENTS
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform hitbox;
    //public Transform offsetCalculator;
    #endregion

    #region HELPER VARIABLES
    private Vector2 workspace;
    private CombatData combatData;
    #endregion

    #region STATUS VARIABLES
    public int facingDirection { get; private set; }
    public bool isDead { get; private set; }
    public bool isStunned { get; private set; }
    #endregion

    #region PLAYER HITPOINTS
    public float currentHitPoints;
    public float currentStunPoints;
    public float currentDecayPoints;
    #endregion

    #region TIMERS
    public float damageImmuneTimer;
    #endregion

    #region UNITY FUNCTIONS
    void Awake()
    {
        stateMachine = new PlayerStateMachine();
        damageImmuneTimer = Time.time;
    }

    void Start()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        InputHandler = GetComponent<PlayerInputHandler>();

        InitializePlayerStateMachine();
        stateMachine.InitializeState(idleState);

        InitializePlayerStatus();
    }

    void Update()
    {
        stateMachine.LogicUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    #endregion

    #region CHECK FUNCTIONS

    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.GD_groundCheckDistance, playerData.GD_whatIsGround | playerData.GD_whatIsPlatform);
    }

    public bool CheckWalled()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, playerData.GD_wallCheckDistance, playerData.GD_whatIsGround | playerData.GD_whatIsPlatform);
    }

    public bool CheckFlip() => InputHandler.NormMovementX != 0 && InputHandler.NormMovementX != facingDirection;

    #endregion

    #region SET FUNCTIONS
    public void SetVelocityX(float xVelocity)
    {
        workspace.Set(xVelocity, Rb.velocity.y);
        Rb.velocity = workspace;
    }

    public void SetVelocityY(float yvelocity)
    {
        workspace.Set(Rb.velocity.x, yvelocity);
        Rb.velocity = workspace;
    }

    public void SetVelocity(Vector2 velocity)
    {
        Rb.velocity = velocity;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    #endregion

    #region ANIMATION CALLBACKS
    public void CheckHitpoint()
    {
        if(attackState != null)
        {
            attackState.CheckHitbox();
            attackState.ConsumeAttackBuffer();
        }
    }

    public void CheckEndAttack()
    {
        if(attackState != null)
        {
            if (attackState.CheckEndAttack())
            {
                attackState.CompleteAttack();
            }
        }
    }

    public void CompleteAttack()
    {
        if (attackState != null)
        {
            attackState.CompleteAttack();
        }
    }

    public void CompleteRoll()
    {
        if(rollState != null)
        {
            rollState.CompleteRoll();
        }
    }

    public void CompleteStun()
    {
        if(stunState != null)
        {
            stunState.ComplementStun();
            isStunned = false;
        }
    }

    public void CompleteDead()
    {
        if(deadState != null)
        {
            deadState.CompleteDead();
        }
    }
    #endregion

    #region MESSAGE FUNCTIONS
    public void Damage(CombatData combatData)
    {
        if (isDead || stateMachine.currentState == rollState)
            return;

        if(stateMachine.currentState == parryState)
        {
            if (combatData.from != null)
            {
                UpdateCombatData();
                combatData.from.SendMessage("Damage", this.combatData);
                // TODO: TO EXECUTION ANIMATION
                stateMachine.SwitchState(idleState);

                var particle = Instantiate(playerData.PS_particle, hitbox.position, playerData.PS_particle.transform.rotation);
                particle.gameObject.transform.Rotate(0, 0, Random.Range(0, 360));
                particle.GetComponent<Animator>().Play("3");
            }
        }
        else if(Time.time > damageImmuneTimer + playerData.GD_damageImmuneTime)
        {
            damageImmuneTimer = Time.time;

            currentHitPoints -= combatData.damage;
            UIEventListener.Instance.OnHpChange(currentHitPoints, playerData.PD_maxHitPoint);

            int dir = (combatData.position.x - transform.position.x > 0 ? -1 : 1);
            workspace.Set(dir * combatData.knockbackDir.x * combatData.knockbackImpulse, combatData.knockbackDir.y * combatData.knockbackImpulse);
            Rb.AddForce(workspace, ForceMode2D.Impulse);

            if (!isStunned)
            {
                currentStunPoints -= combatData.stunDamage;
            }

            if (!isDead && currentHitPoints <= 0)
            {
                isDead = true;
                stateMachine.SwitchState(deadState);
            }
            else if (!isStunned && currentStunPoints <= 0)
            {
                isStunned = true;
                stateMachine.SwitchState(stunState);
                currentStunPoints = playerData.PD_maxStunPoint;
            }
            else
            {
                stateMachine.SwitchState(takeDamageState);
            }

        }
    }

    public void ValidAttack()
    {
        var particle = GameObject.Instantiate(playerData.MAS_meleeAttackParticle, hitbox.position, playerData.PS_particle.transform.rotation);
        particle.gameObject.transform.Rotate(0, 0, Random.Range(0, 360));
        particle.GetComponent<Animator>().Play(Random.Range(0, 3).ToString());
    }
    #endregion

    #region MISC FUNCTIONS
    public void InitializePlayerStateMachine()
    {
        idleState = new PlayerIdleState(stateMachine, this, AlfAnimationHash.IDLE_0, playerData);
        walkState = new PlayerWalkState(stateMachine, this, AlfAnimationHash.RUN_0, playerData);
        jumpState = new PlayerJumpState(stateMachine, this, AlfAnimationHash.JUMP_0, playerData);
        inAirState = new PlayerInAirState(stateMachine, this, AlfAnimationHash.INAIR_0, playerData);
        meleeAttackState = new PlayerMeleeAttackState(stateMachine, this, AlfAnimationHash.ATTACK_0, playerData);
        parryState = new PlayerParryState(stateMachine, this, AlfAnimationHash.PARRY_0, playerData);
        rollState = new PlayerRollState(stateMachine, this, AlfAnimationHash.ROLL_0, playerData);
        stunState = new PlayerStunState(stateMachine, this, AlfAnimationHash.STUN_0, playerData);
        deadState = new PlayerDeadState(stateMachine, this, AlfAnimationHash.DEAD_0, playerData);
        takeDamageState = new PlayerTakeDamageState(stateMachine, this, AlfAnimationHash.TAKEDAMAGE_0, playerData);
        wallState = new PlayerWallState(stateMachine, this, AlfAnimationHash.WALL_0, playerData);
        dashState = new PlayerDashState(stateMachine, this, AlfAnimationHash.DASH_0, playerData);
    }
    public void InitializePlayerStatus()
    {
        // TODO: using a mutable data structure for status reading
        currentHitPoints = playerData.PD_maxHitPoint;
        currentStunPoints = playerData.PD_maxStunPoint;
        currentDecayPoints = GM.GetPlayerDecay();
        // invoke

        facingDirection = 1;
        isDead = false;
        isStunned = false;
    }

    public void UpdateCombatData()
    {
        combatData.damage = playerData.MAS_damageAmount;
        combatData.stunDamage = playerData.MAS_stunAmount;
        combatData.knockbackDir = playerData.MAS_knockbackDirection;
        combatData.knockbackImpulse = playerData.MAS_knockbackImpulse;
        combatData.position = transform.position;
        combatData.from = gameObject;
    }

    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    
    #endregion

    #region AUXILIARY FUNCTIONS
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.GD_groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * playerData.GD_wallCheckDistance);
        Gizmos.DrawWireSphere(hitbox.position, playerData.MAS_hitboxRadius);
    }
    #endregion

}
