using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC2_CombatTrigger : MonoBehaviour
{
    public DragonChase2 dragon;
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            dragon?.Tmp_InitCombat();
            this.gameObject.SetActive(false);
        }
    }
}
