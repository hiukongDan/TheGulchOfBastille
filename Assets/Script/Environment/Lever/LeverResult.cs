using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverResult : MonoBehaviour, IGulchTriggerResult
{
    protected Player player;

    protected bool isInited = false;

    public Lever lever;
    public bool defaultIsOpen = false;
    public bool isNeedSaving = true;
    protected bool isOpened = false;

    protected virtual void OnEnable() {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player && isNeedSaving && !isInited){
            if(!MiscData.gateOpened.ContainsKey(GetHashCode())){
            MiscData.gateOpened.Add(GetHashCode(), defaultIsOpen);
            }
            if(!isInited){
                OnInitLever(MiscData.gateOpened[GetHashCode()]);
            }
        }
        else if(!isInited){
            OnInitLever(defaultIsOpen);
        }
    }

    protected virtual void OnDisable(){
        isInited = false;
    }

    public virtual void OnInitLever(bool isOpen){
        lever.OnLeverInit(isOpen);
        OnInitLeverResult(isOpen);
        isInited = true;
        isOpened = isOpen;
    }

    public virtual void OnInitLeverResult(bool isOpen){
        
    }

    public virtual void OnTriggered(){
        isOpened = !isOpened;
        if(isNeedSaving){
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if(!MiscData.gateOpened.ContainsKey(GetHashCode())){
                MiscData.gateOpened.Add(GetHashCode(), isOpened);
            }
            else{
                MiscData.gateOpened[GetHashCode()] = isOpened;
            }
        }

    }

    public virtual void OnTriggeredEventDone(){
        lever.OnTriggerRefresh();
    }
}
