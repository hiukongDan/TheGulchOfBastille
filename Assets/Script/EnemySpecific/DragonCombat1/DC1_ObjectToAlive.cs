using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_ObjectToAlive : ObjectToAlive
{
    public DC1_TakeoffState takeoffState;
    public void CompleteTakeoff(){
        takeoffState?.Complete();
    }

    public DC1_DiveState diveState;
    public void CompleteDive(){
        diveState?.Complete();
    }

    public void ApplyDiveDamage(){
        diveState?.ApplyDamage();
    }

    public DC1_LandState landState;
    public void CompleteLand(){
        landState?.Complete();
    }

    public override void Flip()
    {
        flipState?.Complete();
    }

    public DC1_SmashState smashState;
    public void CompleteSmash(){
        smashState?.Complete();
    }
    public void ApplySmashDamage(){
        smashState?.ApplyDamage();
    }

    public void ReleaseSmashDust(){
        smashState?.ReleaseSmashDust();
    }

    public DC1_LaserPositionState laserPositionState;
    public void CompleteLaserPosition(){
        laserPositionState?.Complete();
    }

    public DC1_LaserState laserState;

    public void CompletePrepareLaser(){
        laserState?.CompletePrepareLaser();
    }

    public void CompleteLaser(){
        laserState?.Complete();
    }

    public DC1_DieState dieState;

    public void CompleteDieLanding(){
        dieState?.CompleteDieLanding();
    }

}
