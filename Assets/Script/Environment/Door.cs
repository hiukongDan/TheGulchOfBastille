using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public string close_anim;
    public string open_anim;
    
    private Animator anim;
    void Awake(){
        anim = GetComponent<Animator>();
    }

    void OnEnable(){
        Close();
    }

    public void Open(){
        anim.Play(open_anim);
    }

    public void Close(){
        anim.Play(close_anim);
    }
}
