using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected StunStateData data;

    protected float stunStartTime;
    protected bool isStunOver;
    public StunState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, StunStateData stateData) : base(stateMachine, entity, animBoolName)
    {
        data = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
    }

    public override void Enter()
    {
        base.Enter();
        DoChecks();

        entity.objectToAlive.stunState = this;

        stunStartTime = Time.time;
        isStunOver = false;

        entity.anim.SetBool("stun", true);
    }

    public override void Exit()
    {
        base.Exit();

        entity.objectToAlive.stunState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time > stunStartTime + data.stunDuration && !isStunOver)
        {
            isStunOver = true;
            CompleteStunHold();
        }

        if((isGroundDetected || isPlatformDetected) && entity.rb.velocity.y < 0.01f)
        {
            entity.rb.velocity = Vector2.zero;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        DoChecks();
    }

    public void CompleteStunHold() => entity.anim.SetBool("stun", false);

    public virtual void CompleteStun()
    {
        
    }
}
