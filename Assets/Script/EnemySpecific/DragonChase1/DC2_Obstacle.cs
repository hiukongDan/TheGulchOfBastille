using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC2_Obstacle : MonoBehaviour
{
    public GameObject parentObj;
    void Damage(CombatData data)
    {
        if(parentObj != null){
            GameObject.Destroy(parentObj);
        }
        else{
            GameObject.Destroy(gameObject);
        }
    }


}
