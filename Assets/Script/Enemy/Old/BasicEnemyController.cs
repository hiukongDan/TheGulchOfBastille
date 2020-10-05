using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public float walkingSpeed;
    public float knockbackForce;

    public int maxHealth;

    public int knockbackDamage;

    public Vector2 knockbackAngle;

    public GameObject hitparticle,
        deadChunk,
        deadBlood;

    public
    Transform
        groundCheck,
        wallCheck,
        damageBox;


    public float
        groundCheckDistance,
        wallCheckDistance,
        damageBox_width,
        damageBox_height;

    private Vector2 damageBox_leftBtn,
        damageBox_rightTop;

    public LayerMask whatIsGround,
        whatIsPlayer;

    private float flipTimer;

    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private Rigidbody2D rb;

    private Animator anim;

    private State currentState = State.Walking;

    private bool groundDetected;
    private bool wallDetected;

    private int facingDirection = 1;
    private int currentHealth;

    private Transform alive;

    private void Start()
    {
        alive = transform;

        rb = alive.GetComponent<Rigidbody2D>();
        anim = alive.GetComponent<Animator>();

        knockbackAngle.Normalize();
        currentHealth = maxHealth;
        flipTimer = Time.time;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    // -- Walking ----------------------------------------------------------
    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

        if((!groundDetected || wallDetected) && Time.time > flipTimer + 1f)
        {
            Flip();
            flipTimer = Time.time;
        }
        else if(!groundDetected && !wallDetected)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(walkingSpeed * facingDirection, 0);
        }

        CheckDamageBox();
    }

    private void ExitWalkingState()
    {

    }

    // -- Knockback ----------------------------------------------------------
    private void EnterKnockbackState()
    {
        anim.SetBool("knockback", true);
        var particle = Instantiate(hitparticle);
        particle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        particle.transform.position = alive.position;
    }

    private void UpdateKnockbackState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        if (groundDetected && rb.velocity.y < 0.001f)
        {
            anim.SetBool("knockback", false);
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {
        
    }

    // -- Dead --------------------------------------------------------------
    private void EnterDeadState()
    {
        var dc = Instantiate(deadChunk);
        dc.transform.position = alive.position;
        dc.transform.rotation = deadChunk.transform.rotation;
        var db = Instantiate(deadBlood);
        db.transform.position = alive.position;
        db.transform.rotation = deadBlood.transform.rotation;
        Destroy(alive.parent.gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    // -- OTHER FUNCTIONS -------------------------------------------------------
    private void SwitchState(State state)
    {
        if (state == currentState)
            return;

        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.Rotate(new Vector3(0f, 180f));
    }

    private void CheckDamageBox()
    {
        damageBox_leftBtn = new Vector2(damageBox.position.x - damageBox_width / 2, damageBox.position.y - damageBox_height/2);
        damageBox_rightTop = new Vector2(damageBox.position.x + damageBox_width / 2, damageBox.position.y + damageBox_height/2);
        Collider2D collider = Physics2D.OverlapArea(damageBox_leftBtn, damageBox_rightTop, whatIsPlayer);
        if(collider != null)
        {
            float[] data = new float[2];
            data[0] = knockbackDamage;
            data[1] = alive.transform.position.x;
            collider.SendMessage("Damage", data);
        }
    }


    // -- Message --------------------------------------------------
    public void Damage(float[] data)
    {
        if (currentState == State.Knockback)
            return;

        int damage = (int)data[0];
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            SwitchState(State.Dead);
        }
        else
        {
            float playerX = data[1];
            int fromRight = (playerX - alive.position.x >= 0 ? -1 : 1);
            rb.AddForce(new Vector2(fromRight * knockbackForce * knockbackAngle.x, knockbackForce * knockbackAngle.y), ForceMode2D.Impulse);
            SwitchState(State.Knockback);
        }
    }



    // -- EDITOR AIDS --------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireCube(new Vector3(damageBox.position.x, damageBox.position.y), new Vector3(damageBox_width, damageBox_height));
    }
}
