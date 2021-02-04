using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSunTrigger : MonoBehaviour
{
    private LittleSunHandler littleSunHandler;
    void Awake()
    {
        littleSunHandler = transform.parent.GetComponent<LittleSunHandler>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            littleSunHandler.PlayerEnterTrigerArea(collider.GetComponent<Player>(), true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            littleSunHandler.PlayerEnterTrigerArea(collider.GetComponent<Player>(), false);
        }
    }

    
}
