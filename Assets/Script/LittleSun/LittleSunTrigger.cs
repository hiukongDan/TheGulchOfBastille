using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSunTrigger : MonoBehaviour
{
    private LittleSunHandler littleSunHandler;
    private Animator InfoSignAnim;
    void Awake()
    {
        littleSunHandler = transform.GetComponent<LittleSunHandler>();
        InfoSignAnim = transform.parent.Find("InfoSign Parent").GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponent<Player>().SetLittleSunHandler(littleSunHandler);
            InfoSignAnim.Play(InfoSignAnimHash.INTRO);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponent<Player>().SetLittleSunHandler(null);
            InfoSignAnim.Play(InfoSignAnimHash.OUTRO);
        }
    }
}
