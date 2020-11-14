using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newEvadeStateData", menuName="Data/EnemyData/StateData/EvadeStateData")]
public class EvadeStateData : ScriptableObject
{
    public float cooldownTimer = 1f;
}
