using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_ObjectToAlive : ObjectToAlive
{
    public SME1_RecoverState recoverState;
    public SME1_TransformState transformState;

    public Transform sme1_head;
    public Transform sme1_snakeHead;
    private Vector2 sme1_head_initLocalPos;
    private Quaternion sme1_head_initLocalRotation;
    public void Awake(){
        sme1_head_initLocalPos = sme1_head.localPosition;
        sme1_head_initLocalRotation = sme1_head.localRotation;
    }

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

    public void ResetHeadPosition(){
        if(sme1_head != null){
            sme1_head.parent = transform;
            sme1_head.localPosition = sme1_head_initLocalPos;
            sme1_head.localRotation = sme1_head_initLocalRotation;
        }
        SetHeadActive(false);
    }
    
    public void CompleteTransform1()
    {
        transformState?.CompleteTransform();
    }

    public void SetHeadActive(bool active)
    {
        sme1_head?.gameObject.SetActive(active);
    }
}
