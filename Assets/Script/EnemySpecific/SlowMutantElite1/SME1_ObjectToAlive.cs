using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_ObjectToAlive : ObjectToAlive
{
    public SME1_RecoverState recoverState;
    public void CompleteRecover()
    {
        recoverState.CompleteRecover();
    }
}
