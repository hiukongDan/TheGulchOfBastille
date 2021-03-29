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
            if(!player.miscData.gateOpened.ContainsKey(GetHashCode())){
            player.miscData.gateOpened.Add(GetHashCode(), defaultIsOpen);
            }
            if(!isInited){
                OnInitLever(player.miscData.gateOpened[GetHashCode()]);
            }
        }
        else if(!isInited){
            OnInitLever(defaultIsOpen);
        }
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
            if(!player.miscData.gateOpened.ContainsKey(GetHashCode())){
                player.miscData.gateOpened.Add(GetHashCode(), isOpened);
            }
            else{
                player.miscData.gateOpened[GetHashCode()] = isOpened;
            }
        }

    }

    public virtual void OnTriggeredEventDone(){
        lever.OnTriggerRefresh();
    }
}
