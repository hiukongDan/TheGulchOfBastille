using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlfController : MonoBehaviour
{
    public float runningSpeed = 5f;

    public Transform groundCheck;

    public float groundCheckRadius = 1f;

    // STATUS VARIABLES
    private int currentAnimationCode;
    private int facingDirection;

    // CONTROL VARIABLES
    private bool isGrounded;
    private bool canMove;
    private bool isJump;

    // INPUT VARIABLES
    private float horizontal;
    private float vertical;
    private bool pressJump;
    private bool pressAttack;

    private Animator anim;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        canMove = true;
        currentAnimationCode = AlfAnimationHash.IDLE_0;
        anim.Play(AlfAnimationHash.IDLE_0);

        facingDirection = 1;

        isJump = false;
    }

    void FixedUpdate()
    {
        UpdatePhysics();
        CheckSurroundings();
    }

    void Update()
    {
        CheckInput();
        ExecuteInput();
    }

    private void UpdatePhysics()
    {
        if (currentAnimationCode == AlfAnimationHash.IDLE_0)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else if (currentAnimationCode == AlfAnimationHash.IDLE_IRONSWORD)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else if (currentAnimationCode == AlfAnimationHash.RUN_0)
        {
            rb.velocity = new Vector2(runningSpeed * facingDirection, 0f);
        }
        else if (currentAnimationCode == AlfAnimationHash.RUN_IRONSWORD)
        {
            rb.velocity = new Vector2(runningSpeed * facingDirection, 0f);
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);

        if(isGrounded && isJump)
        {
            isJump = false;
        }
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        pressJump = Input.GetButtonDown("Jump");
        pressAttack = Input.GetButton("Fire1");
    }

    private void ExecuteInput()
    {
        if (canMove)
        {
            if (pressAttack && horizontal != 0)
            {
                PlayAnimation(AlfAnimationHash.RUN_IRONSWORD);
                if (Mathf.Sign(horizontal) != facingDirection)
                    Flip();
            }
            else if(pressAttack)
            {
                PlayAnimation(AlfAnimationHash.IDLE_IRONSWORD);
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            else if(horizontal != 0)
            {
                PlayAnimation(AlfAnimationHash.RUN_0);
                if (Mathf.Sign(horizontal) != facingDirection)
                    Flip();
            }
            else
            {
                PlayAnimation(AlfAnimationHash.IDLE_0);
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
    }

    private void PlayAnimation(int animationCode)
    {
        if(animationCode != currentAnimationCode)
        {
            anim.Play(animationCode);
            currentAnimationCode = animationCode;
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(new Vector3(0f, 180f, 0f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(groundCheck.position.x, groundCheck.position.y), groundCheckRadius);
    }

}
