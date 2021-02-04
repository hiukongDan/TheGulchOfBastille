using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSunHandler : MonoBehaviour
{
    public LittleSunData littleSunData;
    private Animator infoSignAnim;
    public Animator LittleSunAnim;
    public Animator BloodAnim;

    void Start()
    {
        infoSignAnim = transform.Find("InfoSign Parent").GetComponentInChildren<Animator>();
        LittleSunAnim = transform.Find("Alive").GetComponent<Animator>();
        BloodAnim = transform.Find("Blood").GetComponent<Animator>();

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
            infoSignAnim.Play(InfoSignAnimHash.INTRO);
        }
        else
        {
            player?.SetLittleSunHandler(null);
            if(infoSignAnim.GetCurrentAnimatorStateInfo(0).shortNameHash != InfoSignAnimHash.EMPTY)
            {
                infoSignAnim.Play(InfoSignAnimHash.OUTRO);
            }
        }
    }


    public bool IsSunActive() => littleSunData.IsActive();
}
