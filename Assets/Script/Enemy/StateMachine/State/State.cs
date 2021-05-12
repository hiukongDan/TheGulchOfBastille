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

    // TODO: why not using AnimationHash?
    public State(FiniteStateMachine stateMachine, Entity entity, string animName, State defaultNextState = null)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        //entity.anim.SetBool(animBoolName, true);
        if(entity.anim.gameObject.activeInHierarchy){
            entity.anim.Play(animName);
            startTime = Time.time;
        }
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

    public virtual void UpdateTimer()
    {

    }

    public virtual bool CanAction()
    {
        return true;
    }

    public virtual void ResetTimer()
    {

    }


    // Complete this state, usually used for setting control boolean
    public virtual void Complete()
    {

    }

    // -- DETECT FUNCTIONS -----------------------------
    // SELECT NECESSARY FUNCTIONS IN CHILD CLASS' DOCHECKS FUNCTION
    public bool DetectPlayerInMeleeAttackRange()
    {
        //var pos = entity.detectCenter ? entity.detectCenter.position : entity.aliveGO.transform.position;
        //Collider2D collider = Physics2D.OverlapBox(new Vector2(pos.x + entity.facingDirection * entity.entityData.meleeAttackDistance / 2, pos.y), new Vector2(entity.entityData.meleeAttackDistance, entity.entityData.meleeAttackDistance), 0, entity.entityData.whatIsPlayer);
        Collider2D collider = Physics2D.Raycast(entity.detectCenter ? entity.detectCenter.position : entity.aliveGO.transform.position, new Vector2(entity.facingDirection, 0), entity.entityData.meleeAttackDistance, entity.entityData.whatIsPlayer).collider;
        if (collider != null)
        {
            detectPlayerInMeleeRange = true;
            detectPlayerTrans = collider.transform;
        }
        else
        {
            detectPlayerInMeleeRange = false;
        }

        return detectPlayerInMeleeRange;
    }

    public bool DetectPlayerInMinAgro()
    {
        //var pos = entity.detectCenter ? entity.detectCenter.position : entity.aliveGO.transform.position;
        //Collider2D collider = Physics2D.OverlapBox(new Vector2(pos.x + entity.facingDirection * entity.entityData.meleeAttackDistance / 2, pos.y), new Vector2(entity.entityData.meleeAttackDistance, entity.entityData.meleeAttackDistance), 0, entity.entityData.whatIsPlayer);
        Collider2D collider = Physics2D.Raycast(entity.detectCenter?entity.detectCenter.position:entity.aliveGO.transform.position, new Vector2(entity.facingDirection, 0), entity.entityData.detectPlayerAgroMinDistance, entity.entityData.whatIsPlayer).collider;
        if (collider != null)
        {
            detectPlayerInMinAgro = true;
            detectPlayerTrans = collider.transform;
        }
        else
        {
            detectPlayerInMinAgro = false;
        }

        return detectPlayerInMinAgro;
    }

    public bool DetectPlayerInMaxAgro()
    {
        //var pos = entity.detectCenter ? entity.detectCenter.position : entity.aliveGO.transform.position;
        //Collider2D collider = Physics2D.OverlapBox(new Vector2(pos.x + entity.facingDirection * entity.entityData.meleeAttackDistance / 2, pos.y), new Vector2(entity.entityData.meleeAttackDistance, entity.entityData.meleeAttackDistance), 0, entity.entityData.whatIsPlayer);
        Collider2D collider = Physics2D.Raycast(entity.detectCenter ? entity.detectCenter.position : entity.aliveGO.transform.position, new Vector2(entity.facingDirection, 0), entity.entityData.detectPlayerAgroMaxDistance, entity.entityData.whatIsPlayer).collider;
        if (collider != null)
        {
            detectPlayerInMaxAgro = true;
            detectPlayerTrans = collider.transform;
        }
        else
        {
            detectPlayerInMaxAgro = false;
        }

        return detectPlayerInMaxAgro;
    }

    public void DetectSurroundings()
    {
        isEdgeDetected = entity.DetectEdge();
        isWallDetected = entity.DetectWall();
        isGroundDetected = entity.DetectGround();
        isPlatformDetected = entity.DetectPlatform();
    }
}
