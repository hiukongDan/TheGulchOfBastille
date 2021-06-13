using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonChaseComplete : MonoBehaviour
{
    public DragonChase2 dragon;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            dragon?.OnDeleteDragon();
        }
    }
}
