using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_LaserPositionState : State
{
    protected DragonCombat1 enemy;
    protected float flyUpSpeed = 2f;
    protected float flyHorSpeed = 3f;
    private Vector2 hoverPos;
    public DC1_LaserPositionState(FiniteStateMachine stateMachine, Entity entity, string animName, DragonCombat1 enemy) : base(stateMachine, entity, animName)
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
        //stateMachine.SwitchState(enemy.laserState);
        stateMachine.SwitchState(enemy.laserState);
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

        // Decide which position to land
        if(enemy.laserLandingPositions.Length > 0){
            hoverPos = enemy.laserLandingPositions[Mathf.FloorToInt(Random.value * enemy.laserLandingPositions.Length)].position;
        }
        else{
            stateMachine.SwitchState(enemy.idleState);
        }

        enemy.dc1_ota.laserPositionState = this;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.dc1_ota.laserPositionState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        DoChecks();

        enemy.FaceToPlayer();

        //Debug.Log("isGroundDetected: " + isGroundDetected);
        if(Gulch.Math.AlmostEqual(enemy.aliveGO.transform.position.y, hoverPos.y, 0.1f)){
            enemy.anim.Play("land_1");
        }
        else{
            Vector2 currentPos = enemy.aliveGO.transform.position;
            enemy.aliveGO.transform.position = new Vector2(
                Mathf.Lerp(currentPos.x, hoverPos.x, Time.deltaTime * flyHorSpeed),
                Mathf.Lerp(currentPos.y, hoverPos.y, Time.deltaTime * flyUpSpeed)
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
