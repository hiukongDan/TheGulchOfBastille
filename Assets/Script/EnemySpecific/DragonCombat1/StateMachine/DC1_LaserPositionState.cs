using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_LaserPositionState : State
{
    protected DragonCombat1 enemy;
    private bool isLanding = false;
    private bool isHover = false;
    protected float flyUpSpeed = 2f;
    protected float flyHorSpeed = 3f;
    protected float landingSpeed = 2f;
    protected float landingOffset = 0.85f;
    private Vector2 landingPos;
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
        isHover = true;

        // Decide which position to land
        if(enemy.laserLandingPositions.Length > 0){
            landingPos = enemy.laserLandingPositions[Mathf.FloorToInt(Random.value * enemy.laserLandingPositions.Length)].position;
            hoverPos = new Vector2(landingPos.x, landingPos.y + landingOffset);
        }
        else{
            stateMachine.SwitchState(enemy.idleState);
        }

        enemy.FaceTo(landingPos);
    }

    public override void Exit()
    {
        base.Exit();

        //enemy.dc1_ota.landState = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        DoChecks();

        //Debug.Log("isGroundDetected: " + isGroundDetected);
        if((isGroundDetected||isPlatformDetected) && Gulch.Math.AlmostEqual(enemy.aliveGO.transform.position.y, landingPos.y, 0.01f) && !isLanding && !isHover){
            enemy.anim.Play("land_0");
            isLanding = true;
            Debug.Log("Landed");
        }
        else if(isHover){
            if(!Gulch.Math.AlmostEqual(hoverPos, enemy.aliveGO.transform.position, 0.1f)){
                Vector2 currentPos = enemy.aliveGO.transform.position;
                enemy.aliveGO.transform.position = new Vector2(
                    Mathf.Lerp(currentPos.x, hoverPos.x, Time.deltaTime * flyHorSpeed),
                    Mathf.Lerp(currentPos.y, hoverPos.y, Time.deltaTime * flyUpSpeed)
                );
            }
            else{
                isHover = false;
                enemy.FaceToPlayer();
            }
        }
        else if(!isLanding){   // find land => find space slightly above player
            Vector2 currentPos = enemy.aliveGO.transform.position;
            enemy.aliveGO.transform.position = new Vector2(currentPos.x,
                Mathf.Lerp(currentPos.y, landingPos.y, Time.deltaTime * landingSpeed)
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
