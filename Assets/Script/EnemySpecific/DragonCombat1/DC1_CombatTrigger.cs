using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_CombatTrigger : MonoBehaviour
{
    public DragonCombat1 dragon;
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            dragon?.Tmp_InitCombat();
            this.gameObject.SetActive(false);
        }
    }
}
