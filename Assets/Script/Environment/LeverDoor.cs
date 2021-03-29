using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoor : LeverResult
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
            animator.Play(AnimationHash.LeverDoorAnimationHash.STAY_OPEN);
        }
        else{
            animator.Play(AnimationHash.LeverDoorAnimationHash.STAY_CLOSE);
        }
    }

    public override void OnTriggered(){
        base.OnTriggered();
        if(isOpened){
            animator.Play(AnimationHash.LeverDoorAnimationHash.OPEN);
        }
        else{
            animator.Play(AnimationHash.LeverDoorAnimationHash.CLOSE);
        }

        Debug.Log("Animator");
    }

    public override void OnTriggeredEventDone(){
        lever.OnTriggerRefresh();
    }

}
