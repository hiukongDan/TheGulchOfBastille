using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    private bool isSpinning;
    void OnEnable()
    {
        isSpinning = false;
        RollIdleAnimation();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !isSpinning){
            isSpinning = true;
            GetComponent<Animator>().Play("wing_spin_0");
        }
    }


    public void RollIdleAnimation(){
        int value = (int)(Random.value * 3);
        value = Mathf.Clamp(value, 0, 3);
        string animStr = "wing_idle_" + value;
        Debug.Log(animStr);
        GetComponent<Animator>()?.Play(animStr);
    }

    public void ResetSpin(){
        isSpinning = false;
    }
}
