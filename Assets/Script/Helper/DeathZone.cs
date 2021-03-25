using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ReloadScene();
        }
        else
        {
            Destroy(collision.collider.transform.parent.gameObject);
        }
    }
}
