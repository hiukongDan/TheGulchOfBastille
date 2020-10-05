using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerData", menuName = "Data/EnemyData/StateData/LookForPlayerStateData")]
public class LookForPlayerStateData : ScriptableObject
{
    public int totalTurnTimes = 2;
    public float waitTime = 2f;
}
