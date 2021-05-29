using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_DieState : State
{
    protected DeadStateData data;
    protected DragonCombat1 enemy;
    private bool isLanding = false;
    private float landingSpeed = 2f;
    public DC1_DieState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, DragonCombat1 enemy) : base(stateMachine, entity, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
    }

    public void CompleteDieLanding(){
        enemy.anim.Play("die_0");
        Debug.Log("die anim");
    }

    public override void Enter()
    {
        base.Enter();
        entity.GetComponent<EnemySaveData>().Save(false);

        enemy.anim.Play("fly_idle_0");

        enemy.dc1_ota.dieState = this;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.dc1_ota.dieState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Fly to init position
        DoChecks();

        Transform targetTrans = enemy.initPosition;
        Vector2 target = new Vector2(targetTrans.position.x,
            targetTrans.position.y);

        //Debug.Log("isGroundDetected: " + isGroundDetected);
        if((isGroundDetected||isPlatformDetected) && Gulch.Math.AlmostEqual(target.y, enemy.aliveGO.transform.position.y, 0.01f) && !isLanding){
            enemy.anim.Play("land_2");
            isLanding = true;
        }
        else if(!isLanding){   // find land => find space slightly above player
            Vector2 currentPos = enemy.aliveGO.transform.position;
            enemy.aliveGO.transform.position = new Vector2(Mathf.Lerp(currentPos.x, target.x, Time.deltaTime * landingSpeed), 
                Mathf.Lerp(currentPos.y, target.y, Time.deltaTime * landingSpeed)
            );
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
