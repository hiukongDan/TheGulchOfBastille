using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_ObjectToAlive : ObjectToAlive
{
    public DC1_TakeoffState takeoffState;
    public void CompleteTakeoff(){
        takeoffState?.Complete();
    }
}
