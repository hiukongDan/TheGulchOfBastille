using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_ObjectToAlive : ObjectToAlive
{
    public SME1_RecoverState recoverState;
    public SME1_TransformState transformState;

    public Transform sme1_head;
    public Transform sme1_snakeHead;

    public void CompleteRecover()
    {
        recoverState?.CompleteRecover();
    }

    public void CompleteTransform0()
    {
        sme1_head?.gameObject.SetActive(true);
    }
    
    public void CompleteTransform1()
    {
        sme1_snakeHead?.gameObject.SetActive(true);
        transformState?.CompleteTransform();
    }
}
