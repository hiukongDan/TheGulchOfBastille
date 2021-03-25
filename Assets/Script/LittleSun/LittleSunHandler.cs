using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSunHandler : MonoBehaviour
{
    public Animator InfoSignAnim{get; private set;}
    public Animator LittleSunAnim{get; private set;}
    public Animator BloodAnim{get; private set;}

    public LittleSunData littleSunData{get; private set;}

    void Awake()
    {
        littleSunData = GetComponent<LittleSunData>();
        InfoSignAnim = transform.Find("InfoSign Parent").GetComponentInChildren<Animator>();
        LittleSunAnim = transform.Find("Alive").GetComponent<Animator>();
        BloodAnim = transform.Find("Blood").GetComponent<Animator>();
    }

    void OnEnable() {
        InitLittleSunState();    
    }

    private void InitLittleSunState()
    {
        if (!littleSunData.IsActive())
        {
            LittleSunAnim.Play(LittleSunAnimHash.BEFORE_ACTIVE_0);
        }
        else
        {
            LittleSunAnim.Play(LittleSunAnimHash.IDLE_0);
        }
    }

    public void OnLittleSunInteraction()
    {
        FindObjectOfType<Player>().playerRuntimeData.lastLittleSunID = littleSunData.LittleSunID;
        if (!littleSunData.IsActive())
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playerCinemaMovement.LightLittleSun(this);
            littleSunData.OnLightLittleSun();
        }
        else
        {
            // Do Other interaction like leveling up or something
        }
    }

    public void PlayerEnterTrigerArea(Player player, bool isEnter)
    {
        if (isEnter)
        {
            player?.SetLittleSunHandler(this);
            InfoSignAnim.Play(InfoSignAnimHash.INTRO);
        }
        else
        {
            player?.SetLittleSunHandler(null);
            if(InfoSignAnim.GetCurrentAnimatorStateInfo(0).shortNameHash != InfoSignAnimHash.EMPTY)
            {
                InfoSignAnim.Play(InfoSignAnimHash.OUTRO);
            }
        }
    }

    public bool IsSunActive() => littleSunData.IsActive();
}
