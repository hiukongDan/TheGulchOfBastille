using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC1_CombatTrigger : MonoBehaviour
{
    public Transform[] enableOnTriggered;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Camera.main.GetComponent<BasicFollower>().UpdateCameraFollowing(transform.parent);
            foreach(Transform trans in enableOnTriggered)
            {
                trans.gameObject.SetActive(true);
                transform.parent.parent.SendMessage("CombatTriggered");
            }
            GetComponent<BoxCollider2D>().enabled = false;
            enabled = false;
        }
    }

    public void ResetTrigger(){
        foreach(Transform trans in enableOnTriggered)
            {
                trans.gameObject.SetActive(false);
            }
            GetComponent<BoxCollider2D>().enabled = true;
    }
}
