using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_Laser : MonoBehaviour
{
    private Animator animator;

    private void RequireAnimator(){
        if(animator == null){
            animator = GetComponentInChildren<Animator>();
        }
    }
    void Start(){
        RequireAnimator();
    }
    public void InitiateLaser(){
        RequireAnimator();
        animator.Play("laser_initiating");
    }

    public void ShowLaser(){
        gameObject.SetActive(true);
    }
    public void HideLaser(){
        gameObject.SetActive(false);
    }
}
