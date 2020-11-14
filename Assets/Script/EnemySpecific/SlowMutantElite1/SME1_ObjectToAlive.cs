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
        if(sme1_head != null)
        {
            sme1_head.parent = transform.parent;
        }
        SetHeadActive(true);
    }
    
    public void CompleteTransform1()
    {
        SetSnakeHeadActive(true);
        transformState?.CompleteTransform();
    }

    public void SetHeadActive(bool active)
    {
        sme1_head?.gameObject.SetActive(active);
    }
    public void SetSnakeHeadActive(bool active)
    {
        sme1_snakeHead?.gameObject.SetActive(active);
    }
}
