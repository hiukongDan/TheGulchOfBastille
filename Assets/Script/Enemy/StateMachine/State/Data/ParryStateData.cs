using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newParryStateData", menuName ="Data/EnemyData/StateData/ParryStateData")]
public class ParryStateData : ScriptableObject
{
    public float cooldownTimer = 2f;
    public float damage = 5f;
}
