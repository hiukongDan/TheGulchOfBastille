using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInformer : MonoBehaviour
{
    public MonoBehaviour DamageInformationDealer;
    void Damage(CombatData data){
        DamageInformationDealer.SendMessage("Damage", data);
    }
}
