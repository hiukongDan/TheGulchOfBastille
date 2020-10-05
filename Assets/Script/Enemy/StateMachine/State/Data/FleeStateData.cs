using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newFleeStateData", menuName="Data/EnemyData/StateData/FleeStateData")]
public class FleeStateData : ScriptableObject
{
    public float fleeSpeed = 1f;
    public float fleeTime = 3f;
}
