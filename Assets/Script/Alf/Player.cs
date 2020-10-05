using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* TODO:
     * replace sphere ground check with something more creditable
     */

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
    // TODO: Add a wall check ?
    public Transform hitbox;
    public Transform offsetCalculator;
    #endregion


    #region HELPER VARIABLES
    private Vector2 workspace;
    #endregion

    #region STATUS VARIABLES
    public int facingDirection { get; private set; }
    #endregion

    #region UNITY FUNCTIONS
    void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }

    void Start()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();

        InputHandler = GetComponent<PlayerInputHandler>();

        idleState = new PlayerIdleState(stateMachine, this, AlfAnimationHash.IDLE_0, playerData);
        walkState = new PlayerWalkState(stateMachine, this, AlfAnimationHash.RUN_0, playerData);
        jumpState = new PlayerJumpState(stateMachine, this, AlfAnimationHash.JUMP_0, playerData);
        inAirState = new PlayerInAirState(stateMachine, this, AlfAnimationHash.JUMP_0, playerData);
        meleeAttackState = new PlayerMeleeAttackState(stateMachine, this, AlfAnimationHash.ATTACK_0, playerData);
        parryState = new PlayerParryState(stateMachine, this, AlfAnimationHash.PARRY_0, playerData);
        rollState = new PlayerRollState(stateMachine, this, AlfAnimationHash.ROLL_0, playerData);

        stateMachine.InitializeState(idleState);

        facingDirection = 1;
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
    #endregion

    #region MESSAGE FUNCTIONS
    public void Damage(CombatData combatData)
    {
        // stub
    }
    #endregion

    #region MISC FUNCTIONS
    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion

    #region AUXILIARIES
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.GD_groundCheckDistance);
        Gizmos.DrawWireSphere(hitbox.position, playerData.MAS_hitboxRadius);
    }

    #endregion

}
