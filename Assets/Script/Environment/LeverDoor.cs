using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : MonoBehaviour, IGulchTriggerResult
{
    public IGulchTrigger trigger;

    public void OnTriggered(){

    }

    public void OnTriggeredEventDone(){
        trigger.OnTriggerRefresh();
    }

}
