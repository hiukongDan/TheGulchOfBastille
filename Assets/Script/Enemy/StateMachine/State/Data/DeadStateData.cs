using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/EnemyData/StateData/DeadStateData")]
public class DeadStateData : ScriptableObject
{
    public GameObject deadChunkParticle;
    public GameObject deadBloodParticle;

    public float disappearTime = 1f;
}
