using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_TriggerEventHandler : MonoBehaviour
{
    void Start()
    {
//        if(PlayerPrefs.GetInt("SME_active", -1) == -1)
//        {
            transform.Find("Alive/StoneRecover Trigger").gameObject.SetActive(false);
            Gulch.GulchMainEventListener.Instance.Slay_SlowMutant_TrainingGround += OnTwoSlowMutantDead;
//        }
    }
    public void OnTwoSlowMutantDead()
    {
        Gulch.GulchMainEventListener.Instance.Slay_SlowMutant_TrainingGround -= OnTwoSlowMutantDead;
/*
        if (PlayerPrefs.GetInt("SME_active", -1) == -1)
        {
            PlayerPrefs.SetInt("SME_active", 1);
        }*/

        transform.Find("Alive/StoneRecover Trigger").gameObject.SetActive(true);
    }
}
