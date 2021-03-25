using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    // Start is called before the first frame update
    public string grass_anim;
    private Animator anim;
    void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            anim.Play(grass_anim);
        }
    }

}
