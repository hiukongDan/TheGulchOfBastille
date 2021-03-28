using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IGulchTrigger
{
    public bool isOn = false;
    public IGulchTriggerResult triggerResult;
    protected Animator anim;
    protected bool isTriggerActive;
    void Awake(){
        anim = GetComponentInChildren<Animator>();
    }

    public void OnEnable(){
        isTriggerActive = true;
    }

    public void OnLeverOn(){
        anim.Play("lever_on_0");
    }

    public void OnLeverOff(){
        anim.Play("lever_off_0");
    }

    public void Damage(CombatData combatData){
        if(combatData.from.tag == "Player" && isTriggerActive){
            if(isOn){
                OnLeverOff();
            }
            else{
                OnLeverOn();
            }

            triggerResult.OnTriggered();

            isTriggerActive = false;
            isOn = !isOn;
        }
    }

    public void OnTriggerRefresh(){
        isTriggerActive = true;
    }
}
