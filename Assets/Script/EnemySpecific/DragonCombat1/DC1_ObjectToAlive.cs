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


}
