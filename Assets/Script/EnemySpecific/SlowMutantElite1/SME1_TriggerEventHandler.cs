using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_TriggerEventHandler : MonoBehaviour
{
    void Start()
    {
        transform.Find("Alive/StoneRecover Trigger").gameObject.SetActive(false);
        Gulch.GulchMainEventListener.Instance.Slay_SlowMutant_TrainingGround += OnTwoSlowMutantDead;
    }
    public void OnTwoSlowMutantDead()
    {
        Gulch.GulchMainEventListener.Instance.Slay_SlowMutant_TrainingGround -= OnTwoSlowMutantDead;

        transform.Find("Alive/StoneRecover Trigger").gameObject.SetActive(true);
    }
}
