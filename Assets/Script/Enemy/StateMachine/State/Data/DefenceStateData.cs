using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newDefenceStateData",menuName="Data/EnemyData/StateData/DefenceStateData")]
public class DefenceStateData : ScriptableObject
{
    public float defenceDuration = 1f;
    public float defenceCooldown = 3f;
}
