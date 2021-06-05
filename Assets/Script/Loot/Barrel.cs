using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float MaxHp = 100;
    private float currentHp = 0;
    public List<Sprite> stateSprites;
    private int currentState;

    void Reset(){
        MaxHp = currentHp;
    }
    void Update()
    {
        
    }

    void Damage(CombatData data){

    }
}
