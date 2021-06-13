using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAreaPlayerDetect : MonoBehaviour
{
    private SubAreaHandler subAreaHandler;
    private Animator InfoSignAnim;
    void Awake()
    {
        subAreaHandler = transform.parent.GetComponent<SubAreaHandler>();
        InfoSignAnim = transform.parent.GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(subAreaHandler.IsTransitionAutomatically){
                subAreaHandler.OnPerformAction();
            }
            else{
                collider.gameObject.GetComponent<Player>()?.SetSubAreaHandler(subAreaHandler);
                InfoSignAnim.Play(InfoSignAnimHash.INTRO);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Player player = collider.gameObject.GetComponent<Player>();
            if(!subAreaHandler.IsTransitionAutomatically && player?.stateMachine.currentState != player?.cinemaState){
                player.SetSubAreaHandler(null);
                if(InfoSignAnim.gameObject.activeInHierarchy){
                    InfoSignAnim.Play(InfoSignAnimHash.OUTRO);
                }
            }
        }
    }
}
