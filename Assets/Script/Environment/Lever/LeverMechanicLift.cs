using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverMechanicLift : LeverResult
{
    protected Animator animator;
    protected void Awake(){
        animator = GetComponent<Animator>();
    }
    protected override void OnEnable(){
        base.OnEnable();
    }
    public override void OnInitLeverResult(bool isOpen)
    {
        base.OnInitLeverResult(isOpen);
        if(isOpen){
            animator.Play(AnimationHash.MechanicLiftHash.Stay_Up);
        }
        else{
            animator.Play(AnimationHash.MechanicLiftHash.Stay_Down);
        }
    }

    public override void OnTriggered(){
        base.OnTriggered();
        if(isOpened){
            animator.Play(AnimationHash.MechanicLiftHash.Go_Down);
        }
        else{
            animator.Play(AnimationHash.MechanicLiftHash.Go_Up);
        }

        // Debug.Log("Animator");
    }

    public override void OnTriggeredEventDone(){
        lever.OnTriggerRefresh();
    }
}
