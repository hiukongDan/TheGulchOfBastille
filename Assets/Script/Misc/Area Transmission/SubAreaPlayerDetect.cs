using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAreaPlayerDetect : MonoBehaviour
{
    private SubAreaHandler subAreaHandler;
    private Animator InfoSignAnim;
    void Start()
    {
        subAreaHandler = GetComponent<SubAreaHandler>();
        InfoSignAnim = transform.parent.GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().SetSubAreaHandler(subAreaHandler);
            InfoSignAnim.Play(InfoSignAnimHash.INTRO);
        }   
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().SetSubAreaHandler(null);
            InfoSignAnim.Play(InfoSignAnimHash.OUTRO);
        }
    }
}
