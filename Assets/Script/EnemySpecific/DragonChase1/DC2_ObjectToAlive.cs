using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC2_ObjectToAlive : ObjectToAlive
{
    public DC2_SleepState sleepState;
    public void CompleteWake()
    {
        sleepState?.Complete();
    }
}
