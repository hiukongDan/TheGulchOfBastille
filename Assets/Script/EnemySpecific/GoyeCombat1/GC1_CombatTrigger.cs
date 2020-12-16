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
            }

            GameObject.Destroy(this);
        }
    }
}
