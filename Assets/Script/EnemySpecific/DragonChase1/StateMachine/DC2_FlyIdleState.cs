using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC2_FlyIdleState : DC1_FlyIdleState
{
    protected DragonChase2 dragon;
    public DC2_FlyIdleState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonChase2 dragon) 
    : base(stateMachine, entity, animName, null)
    {
        this.dragon = dragon;
        this.idleMaxTime = 5f;
    }

    public override void LogicUpdate()
    {
        Vector2 playerDir = dragon.refPlayer.transform.position - dragon.aliveGO.transform.position;

        /* I felt like the dragon is like a pet :) in these codes */
        Vector2 currentPos = dragon.aliveGO.transform.position;

        float hoverOffset = 5f;

        dragon.FaceToPlayer();
        dragon.aliveGO.transform.position = new Vector2(Mathf.Lerp(currentPos.x, 
            dragon.refPlayer.transform.position.x, flyUpSpeed * Time.deltaTime), 
            Mathf.Lerp(currentPos.y, dragon.refPlayer.transform.position.y + hoverOffset,
            flyUpSpeed * Time.deltaTime));

        if(this.startTime + idleMaxTime < Time.time){
            stateMachine.SwitchState(dragon.diveState);
        }
    }


}
