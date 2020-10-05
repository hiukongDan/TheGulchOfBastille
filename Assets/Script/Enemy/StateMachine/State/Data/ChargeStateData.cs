using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/EnemyData/StateData/ChargeStateData")]
public class ChargeStateData : ScriptableObject
{
    public float chargeSpeed = 5;
    public float chargeDuration = 1f;
}
