using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class SM1_IdleState : IdleState
{
    protected SlowMutant1 enemy;

    private float idleStayTimeInLookup;
    private float idleStayTimeInLookfront;

    private float changeIdleAnimationTimer;

    private Vector2 changeIdleAnimationTimeRange;

    private bool isLookFront;

    private float idleStartTime;

    public SM1_IdleState(FiniteStateMachine stateMachine, Entity entity, string animName, IdleStateData stateData, SlowMutant1 enemy, float idleStayTimeInLookUp, float idleStayTimeInLookfront, Vector2 changeIdleAnimationTimeRange) : base(stateMachine, entity, animName, stateData)
    {
        this.enemy = enemy;
        this.idleStayTimeInLookfront = idleStayTimeInLookfront;
        this.idleStayTimeInLookup = idleStayTimeInLookUp;
        this.changeIdleAnimationTimeRange = changeIdleAnimationTimeRange;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        idleStartTime = Time.time;
        isLookFront = true;
        enemy.anim.SetBool("isLookFront", isLookFront);
        changeIdleAnimationTimer = Random.Range(changeIdleAnimationTimeRange.x, changeIdleAnimationTimeRange.y);

        enemy.rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isLookFront)
        {
            if (changeIdleAnimationTimer < 0)
            {
                enemy.anim.SetFloat("idle_blend", Random.value);
                changeIdleAnimationTimer = Random.Range(changeIdleAnimationTimeRange.x, changeIdleAnimationTimeRange.y);
            }
            else
            {
                changeIdleAnimationTimer -= Time.deltaTime;
            }
        }

        if (detectPlayerInMinAgro)
        {
            enemy.detectPlayerState.SetPlayerDetectedTrans(detectPlayerTrans);
            stateMachine.SwitchState(enemy.detectPlayerState);
        }
        // TODO: A hotfix, should be move to another state later
        else if ((isGroundDetected || isPlatformDetected) && entity.rb.velocity.y < 0.01f)
        {
            stateMachine.SwitchState(enemy.idleState);
        }
        else if(Time.time > idleStartTime + (isLookFront ? idleStayTimeInLookfront : idleStayTimeInLookup))
        {
            idleStartTime = Time.time;
            isLookFront = !isLookFront;
            enemy.anim.SetBool("isLookFront", isLookFront);

            if (isLookFront)
            {
                changeIdleAnimationTimer = Random.Range(changeIdleAnimationTimeRange.x, changeIdleAnimationTimeRange.y);
            }
        }
        // TRANSIT TO OTHER STATE
        else if (Time.time > startTime + idleDurationTime)
        {
            stateMachine.SwitchState(enemy.walkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
