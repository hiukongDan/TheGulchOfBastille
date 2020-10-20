using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlayerDetect : MonoBehaviour
{
    private NPCEventHandler npcEventHandler;
    void Start()
    {
        npcEventHandler = GetComponent<NPCEventHandler>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            npcEventHandler.OnNPCEnterInteraction();
            collider.gameObject.GetComponent<Player>().SetNPCEventHandler(npcEventHandler);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            npcEventHandler.OnNPCExitInteraction();
            collider.gameObject.GetComponent<Player>().SetNPCEventHandler(null);
        }
    }

}
