using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    /* TODO:
     * replace sphere ground check with something more creditable
     */

    #region ENUM
    public enum AC_TYPE{
        NORMAL, ROOT_MOTION,
    };

    #endregion
    #region REFERENCES
    private GameManager GM;
    #endregion

    #region MISC VARIABLES
    public float pixelsPerUnits = 32;
    #endregion

    #region STATEMACHINE
    public PlayerStateMachine stateMachine;
    public PlayerStateCooldownTimer stateCooldownTimer;
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
    public PlayerCinemaState cinemaState { get; private set; }
    public PlayerLadderState ladderState{get; private set;}

    // TODO: REMOVE STUN STATE
    public PlayerStunState stunState { get; private set; }
    public PlayerTakeDamageState takeDamageState { get; private set; }
    public PlayerConverseState converseState { get; private set; }

    public D_PlayerStateMachine playerData;
    public D_PlayerAbility playerAbilityData;
    #endregion

    #region ATTACHED STATES
    public PlayerAttackState attackState;
    #endregion

    #region COMPONENTS
    public RuntimeAnimatorController ACRootmotion;
    public RuntimeAnimatorController ACNormal;
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D Bc {get; private set;}
    public SpriteRenderer Sr{get; private set;}
    public PlayerInputHandler InputHandler { get; private set; }
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform hitbox;
    public Transform LadderEndCheck;
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
    public PlayerRuntimeData playerRuntimeData = new PlayerRuntimeData();
    #endregion


    #region TIMERS
    public float damageImmuneTimer;
    #endregion

    #region CONVENIENT VARIABLES
    public int FACE_LEFT = -1;
    public int FACE_RIGHT = 1;
    #endregion

    #region UNITY FUNCTIONS
    void Awake()
    {
        stateMachine = new PlayerStateMachine();
        stateCooldownTimer = new PlayerStateCooldownTimer();

        stateMachine.SetStateCooldownTimer(stateCooldownTimer);

        damageImmuneTimer = Time.time;
        
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Bc = GetComponent<BoxCollider2D>();
        Sr = GetComponent<SpriteRenderer>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        Anim.runtimeAnimatorController = ACNormal;

        InputHandler = GetComponent<PlayerInputHandler>();

        playerRuntimeData.InitPlayerRuntimeData(playerData);
    }

    void Start()
    {
        InitializePlayerStateMachine();
        stateMachine.InitializeState(idleState);

        InitializePlayerStatus();
    }

    void Update()
    {
        if(GM.CanPlayerAction())
            stateMachine.LogicUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();

        playerRuntimeData.lastPosition = transform.position;
    }

    void OnAnimatorMove(){
        if(Anim.runtimeAnimatorController == ACRootmotion && Anim.applyRootMotion){
            Rb.velocity = Anim.deltaPosition / Time.deltaTime;
        }
    }
    void OnApplicationQuit(){
        
    }
    void OnDisable() {
        
    }

    void OnDestroy() {
        
    }
    

    #endregion

    #region CHECK FUNCTIONS

    public bool CheckGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, playerData.GD_groundCheckDistance, playerData.GD_whatIsGround | playerData.GD_whatIsPlatform);
    }

    public bool CheckWalled(){
        return Physics2D.Raycast(wallCheck.position, transform.right, playerData.GD_wallCheckDistance, playerData.GD_whatIsGround);
    }

    public bool CheckLadderEnd(){
        return Physics2D.OverlapCircle(LadderEndCheck.position, playerData.GD_ladderEndCheckRadius, playerData.GD_whatIsLadder);
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
            // attackState.ConsumeAttackBuffer();
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
        attackState.CompleteAttack();
    }

    public void CompleteRoll()
    {
        rollState?.CompleteRoll();
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
        deadState?.CompleteDead();
    }

    public void CompleteParry()
    {
        parryState?.CompleteParry();
    }

    public void EnterParryValid()
    {
        parryState?.EnterParryValid();
    }

    public void ExitParryValid()
    {
        parryState?.ExitParryValid();
    }

    public bool lightingLittleSunToken = false;
    public void StartLightingLittleSun()
    {
        lightingLittleSunToken = false;
    }
    public void FinishLightingLittleSun()
    {
        lightingLittleSunToken = true;
    }

    public void CompleteStartClimbLadder(){
        ladderState?.CompleteStartClimbLadder();
    }

    public void CompleteClimbLadder(){
        ladderState?.CompleteClimbLadder();
    }
    public void CompleteLanding(){
        inAirState?.CompleteLanding();
    }

    #endregion

    #region MESSAGE FUNCTIONS
    public void Damage(CombatData combatData)
    {
        if (isDead || stateMachine.currentState == rollState)
            return;

        if(stateMachine.currentState == parryState && parryState.IsParryValid)
        {
            if (combatData.from != null)
            {
                UpdateCombatData();
                this.combatData.isParryDamage = true;
                this.combatData.damage = playerData.PS_damage;
                combatData.from.SendMessage("Damage", this.combatData);
                // TODO: TO EXECUTION ANIMATION
                stateMachine.SwitchState(idleState);

                var particle = Instantiate(playerData.PS_particle, hitbox.position, playerData.PS_particle.transform.rotation);
                particle.gameObject.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 360));
                particle.GetComponent<Animator>().Play("3");
            }
        }
        else if(Time.time > damageImmuneTimer + playerData.GD_damageImmuneTime)
        {
            damageImmuneTimer = Time.time;

            playerRuntimeData.currentHitPoints -= combatData.damage;
            UIEventListener.Instance.OnHpChange(playerRuntimeData.currentHitPoints, playerData.PD_maxHitPoint);

            int dir = (combatData.position.x - transform.position.x > 0 ? -1 : 1);
            workspace.Set(dir * combatData.knockbackDir.x * combatData.knockbackImpulse, combatData.knockbackDir.y * combatData.knockbackImpulse);
            Rb.velocity = Vector2.zero;
            Rb.AddForce(workspace, ForceMode2D.Impulse);

            if (combatData.isParryDamage)
            {
                var particle = Instantiate(playerData.PS_particle, combatData.position, playerData.PS_particle.transform.rotation);
                particle.gameObject.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 360));
                particle.GetComponent<Animator>().Play("3");
            }

            if (!isStunned)
            {
                playerRuntimeData.currentStunPoints -= combatData.stunDamage;
            }

            if (!isDead && playerRuntimeData.currentHitPoints <= 0)
            {
                isDead = true;
                stateMachine.SwitchState(deadState);
            }
            else if (!isStunned && playerRuntimeData.currentStunPoints <= 0)
            {
                isStunned = true;
                stateMachine.SwitchState(stunState);
                playerRuntimeData.currentStunPoints = playerData.PD_maxStunPoint;
            }
            else
            {
                stateMachine.SwitchState(takeDamageState);
            }

            Gulch.GameEventListener.Instance.OnTakeDamage(new Gulch.TakeDamageData(gameObject, Gulch.SpriteEffectType.Blink));
        }
    }

    public void ValidAttack()
    {
        var particle = GameObject.Instantiate(playerData.MAS_meleeAttackParticle, hitbox.position, playerData.PS_particle.transform.rotation);
        particle.gameObject.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 360));
        particle.GetComponent<Animator>().Play(UnityEngine.Random.Range(0, 3).ToString());
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
        parryState = new PlayerParryState(stateMachine, this, AlfAnimationHash.PARRY_1, playerData);
        rollState = new PlayerRollState(stateMachine, this, AlfAnimationHash.ROLL_0, playerData);
        stunState = new PlayerStunState(stateMachine, this, AlfAnimationHash.STUN_0, playerData);
        deadState = new PlayerDeadState(stateMachine, this, AlfAnimationHash.DEAD_0, playerData);
        takeDamageState = new PlayerTakeDamageState(stateMachine, this, AlfAnimationHash.TAKEDAMAGE_0, playerData);
        wallState = new PlayerWallState(stateMachine, this, AlfAnimationHash.WALL_0, playerData);
        dashState = new PlayerDashState(stateMachine, this, AlfAnimationHash.DASH_0, playerData);
        converseState = new PlayerConverseState(stateMachine, this, AlfAnimationHash.IDLE_0, playerData);
        cinemaState = new PlayerCinemaState(stateMachine, this, AlfAnimationHash.IDLE_0, playerData);
        ladderState = new PlayerLadderState(stateMachine, this, AlfAnimationHash.IDLE_0, playerData);

        InitializePlayerCooldownTimer();
    }

    private void InitializePlayerCooldownTimer()
    {
        stateCooldownTimer.AddStateTimer(meleeAttackState);
        stateCooldownTimer.AddStateTimer(parryState);
        stateCooldownTimer.AddStateTimer(rollState);

        stateCooldownTimer.AddStateTimer(dashState);
    }

    public void InitializePlayerStatus()
    {
        facingDirection = FACE_RIGHT;
        isDead = false;
        isStunned = false;

        if(playerRuntimeData.isLoaded){
            transform.position = playerRuntimeData.lastPosition;
            UIEventListener.Instance.OnHpChange(playerRuntimeData.currentHitPoints, playerData.PD_maxHitPoint);
            UIEventListener.Instance.OnDpChange(playerRuntimeData.currentDecayPoints, playerData.PD_maxDecayPoint);
        }
    }
    public void ResetGrounded(){
        InputHandler.ResetIsJump();
        jumpState.ResetJumpAmountLeft();
        wallState.ResetWallJumpAmountLeft();
        dashState.ResetDashAmountLeft();
    }

    public void UpdateCombatData()
    {
        combatData.damage = playerData.MAS_damageAmount;
        combatData.stunDamage = playerData.MAS_stunAmount;
        combatData.knockbackDir = playerData.MAS_knockbackDirection;
        combatData.knockbackImpulse = playerData.MAS_knockbackImpulse;
        combatData.position = transform.position;
        combatData.from = gameObject;
        combatData.facingDirection = facingDirection;
    }

    // RETURN: true if actually flipped
    public bool FaceTo(Vector2 targetPos)
    {
        if (targetPos.x - transform.position.x > 0 != facingDirection > 0)
        {
            Flip();
            return true;
        }
        return false;
    }

    public void Flip()
    {
        facingDirection *= -1;
        // transform.Rotate(0f, 180f, 0f);
        Sr.flipX = !Sr.flipX;
    }

    /// <Summary>
    /// Switch Animator from rootMotion enabled one to rootMotion disabled one, or vice versa.
    /// </Summary>
    public void SwitchAC(AC_TYPE type){
        if(type == AC_TYPE.NORMAL){
            if(Anim.runtimeAnimatorController != ACNormal){
                Anim.runtimeAnimatorController = ACNormal;
                Anim.applyRootMotion = false;
                Anim.updateMode = AnimatorUpdateMode.Normal;

                //Rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        else if(type == AC_TYPE.ROOT_MOTION){
            if(Anim.runtimeAnimatorController != ACRootmotion){
                Anim.runtimeAnimatorController = ACRootmotion;
                Anim.applyRootMotion = true;
                Anim.updateMode = AnimatorUpdateMode.AnimatePhysics;

                //Rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
    #endregion

    #region INTERFACE
    private NPCEventHandler npcEventHandler;
    public void SetNPCEventHandler(NPCEventHandler npcEventHandler) => this.npcEventHandler = npcEventHandler;
    public NPCEventHandler GetNPCEventHandler() => npcEventHandler;

    private SubAreaHandler subAreaHandler;
    public void SetSubAreaHandler(SubAreaHandler subAreaHandler) => this.subAreaHandler = subAreaHandler;
    public SubAreaHandler GetSubAreaHandler() => subAreaHandler;

    private LittleSunHandler littleSunHandler;
    public void SetLittleSunHandler(LittleSunHandler littleSunHandler) => this.littleSunHandler = littleSunHandler;
    public LittleSunHandler GetLittleSunHandler() => littleSunHandler;
    #endregion

    #region EVENT
    public event Action EndConversation;

    public void OnEndConversation()
    {
        EndConversation?.Invoke();
    }
    #endregion

    #region AUXILIARY FUNCTIONS
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.GD_groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * playerData.GD_wallCheckDistance);
        Gizmos.DrawWireSphere(hitbox.position, playerData.MAS_hitboxRadius);
        Gizmos.DrawWireSphere(LadderEndCheck.position, playerData.GD_ladderEndCheckRadius);
    }
    #endregion

}
