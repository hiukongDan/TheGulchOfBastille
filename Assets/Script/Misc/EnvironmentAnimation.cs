using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnimation : MonoBehaviour
{
    private Animator animator;
    public string clipName;
    void Awake(){
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void OnEnable(){
        animator.Play(clipName);
    }
}
