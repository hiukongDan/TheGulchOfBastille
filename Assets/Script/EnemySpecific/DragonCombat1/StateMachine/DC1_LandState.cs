using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_LandState : State
{
    protected DragonCombat1 enemy;
    private bool isLanding = false;
    protected float flyUpSpeed = 2f;
    public DC1_LandState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy) : base(stateMachine, entity, animName)
    {
        this.enemy = enemy;
    }

    public override bool CanAction()
    {
        return base.CanAction();
    }

    public override void Complete()
    {
        base.Complete();
        stateMachine.SwitchState(enemy.idleState);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
    }

    public override void Enter()
    {
        base.Enter();

        // fly for landing
        enemy.anim.Play("fly_idle_0");

        isLanding = false;

        enemy.dc1_ota.landState = this;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.dc1_ota.landState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        DoChecks();

        float landingOffset = 0.87f;
        Vector2 target = new Vector2(enemy.refPlayer.transform.position.x,
            enemy.refPlayer.transform.position.y + landingOffset);

        //Debug.Log("isGroundDetected: " + isGroundDetected);
        if(isGroundDetected && Gulch.Math.AlmostEqual(target.y, enemy.aliveGO.transform.position.y, 0.1f) && !isLanding){
            enemy.anim.Play("land_0");
            isLanding = true;
        }
        else if(!isLanding){   // find land => find space slightly above player
            Vector2 currentPos = enemy.aliveGO.transform.position;
            enemy.aliveGO.transform.position = new Vector2(currentPos.x,
                Mathf.Lerp(currentPos.y, target.y, Time.deltaTime * flyUpSpeed)
            );
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer()
    {

    }

    public override void UpdateTimer()
    {
        base.UpdateTimer();
    }
}
