using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected string animName;

    protected bool detectPlayerInMinAgro;
    protected bool detectPlayerInMaxAgro;
    protected bool detectPlayerInMeleeRange;
    protected Transform detectPlayerTrans;

    protected bool isWallDetected;
    protected bool isEdgeDetected;
    protected bool isGroundDetected;
    protected bool isPlatformDetected;

    protected float startTime;

    public State(FiniteStateMachine stateMachine, Entity entity, string animName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        //entity.anim.SetBool(animBoolName, true);
        entity.anim.Play(animName);
        startTime = Time.time;
    }

    public virtual void Exit()
    {
        //entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void DoChecks()
    {

    }

    // -- DETECT FUNCTIONS -----------------------------
    // SELECT NECESSARY FUNCTIONS IN CHILD CLASS' DOCHECKS FUNCTION
    public void DetectPlayerInMeleeAttackRange()
    {
        Collider2D collider = Physics2D.Raycast(entity.aliveGO.transform.position, entity.aliveGO.transform.right, entity.entityData.meleeAttackDistance, entity.entityData.whatIsPlayer).collider;
        if (collider != null)
        {
            detectPlayerInMeleeRange = true;
            detectPlayerTrans = collider.transform;
        }
        else
        {
            detectPlayerInMeleeRange = false;
        }
    }

    public void DetectPlayerInMinAgro()
    {
        Collider2D collider = Physics2D.Raycast(entity.aliveGO.transform.position, entity.aliveGO.transform.right, entity.entityData.detectPlayerAgroMinDistance, entity.entityData.whatIsPlayer).collider;
        if (collider != null)
        {
            detectPlayerInMinAgro = true;
            detectPlayerTrans = collider.transform;
        }
        else
        {
            detectPlayerInMinAgro = false;
        }
    }

    public void DetectPlayerInMaxAgro()
    {
        Collider2D collider = Physics2D.Raycast(entity.aliveGO.transform.position, entity.aliveGO.transform.right, entity.entityData.detectPlayerAgroMaxDistance, entity.entityData.whatIsPlayer).collider;
        if (collider != null)
        {
            detectPlayerInMaxAgro = true;
            detectPlayerTrans = collider.transform;
        }
        else
        {
            detectPlayerInMaxAgro = false;
        }
    }

    public void DetectSurroundings()
    {
        isEdgeDetected = entity.DetectEdge();
        isWallDetected = entity.DetectWall();
        isGroundDetected = entity.DetectGround();
        isPlatformDetected = entity.DetectPlatform();
    }
}
