using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_FlyIdleState : State
{
    protected DragonCombat1 enemy;
    protected State prevState;
    protected float flyUpSpeed = 1f;
    protected float distanceKeep = 2f;
    protected float idleMaxTime = 1f;
    public DC1_FlyIdleState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy) : base(stateMachine, entity, animName)
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
    }


    public override void DoChecks()
    {
        base.DoChecks();
        DetectSurroundings();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Vector2 playerDir = enemy.refPlayer.transform.position - enemy.aliveGO.transform.position;

        /* I felt like the dragon is like a pet :) in these codes */
        Vector2 currentPos = enemy.aliveGO.transform.position;

        if(playerDir.magnitude < Mathf.Sqrt(distanceKeep*distanceKeep)){    // keep away from player
            enemy.aliveGO.transform.position = new Vector2(Mathf.Lerp(currentPos.x, 
                enemy.refPlayer.transform.position.x+distanceKeep*(-enemy.facingDirection),
                flyUpSpeed * Time.deltaTime), 
                Mathf.Lerp(currentPos.y, 
                enemy.refPlayer.transform.position.y+distanceKeep, flyUpSpeed * Time.deltaTime));
        }
        else{   // stay close to player above
            enemy.FaceToPlayer();
            enemy.aliveGO.transform.position = new Vector2(Mathf.Lerp(currentPos.x, 
                enemy.refPlayer.transform.position.x,
                flyUpSpeed * Time.deltaTime),
                currentPos.y);
        }

        if(this.startTime + idleMaxTime < Time.time){
            DecideFlyAttack();
        }
    }

    void DecideFlyAttack(){
        if(Random.value > 0.5f){
                // dive
                stateMachine.SwitchState(enemy.diveState);
            }
            else{
                // or laser attack
                stateMachine.SwitchState(enemy.laserPositionState);
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
