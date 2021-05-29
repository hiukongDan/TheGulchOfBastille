using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IGulchTrigger
{
    public LeverResult leverResult;
    protected Animator anim;
    protected bool isTriggerActive;
    protected bool isOpened;
    void Awake(){
        
    }

    public void OnEnable(){
        
        isTriggerActive = true;
    }

    public void OnLeverInit(bool isOpen){
        if(anim == null){
            anim = GetComponent<Animator>();
        }
        if(isOpen){
            anim.Play(AnimationHash.LeverAnimationHash.STAY_OFF);
        }
        else{
            anim.Play(AnimationHash.LeverAnimationHash.STAY_ON);
        }
        isOpened = isOpen;
    }

    public void OnLeverInteraction(bool isOpen){
        if(isOpen){
            anim.Play(AnimationHash.LeverAnimationHash.OFF);
        }
        else{
            anim.Play(AnimationHash.LeverAnimationHash.ON);
        }
        isOpened = isOpen;
    }

    public void Damage(CombatData combatData){
        if(combatData.from != null && combatData.from.tag == "Player" && isTriggerActive){
            OnLeverInteraction(!isOpened);
            leverResult.OnTriggered();
            isTriggerActive = false;
        }
    }

    public void OnTriggerRefresh(){
        isTriggerActive = true;
    }
}
