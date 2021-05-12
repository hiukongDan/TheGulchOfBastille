using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_FlyIdleState : State
{
    protected DragonCombat1 enemy;
    protected State prevState;
    protected float flyUpSpeed = 0.5f;
    protected float distanceKeep = 1f;
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

        enemy.FaceToPlayer();

        //Vector2 playerDir = enemy.refPlayer.transform.position - enemy.transform.position;

        // if  player is above, fly to higher position
        // if(Vector2.Angle(playerDir, Vector2.up) * Mathf.Deg2Rad < Mathf.PI/2){
        //     Vector2 currentPos = enemy.transform.position;
        //     enemy.transform.position = new Vector2(currentPos.x, Mathf.Lerp(currentPos.y, enemy.refPlayer.transform.y+distanceKeep, .5f));
        // }

        /* I felt like the dragon is like a pet :) in these codes */
        Vector2 currentPos = enemy.transform.position;
        enemy.transform.position = new Vector2(Mathf.Lerp(currentPos.x, 
            enemy.refPlayer.transform.position.x+distanceKeep*(-enemy.facingDirection),
            flyUpSpeed * Time.deltaTime), 
            Mathf.Lerp(currentPos.y, 
            enemy.refPlayer.transform.position.y+distanceKeep, flyUpSpeed * Time.deltaTime));

        //Debug.Log(playerDir);
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
