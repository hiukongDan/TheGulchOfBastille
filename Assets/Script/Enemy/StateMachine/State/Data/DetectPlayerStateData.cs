using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "newDetectPlayerStateData", menuName = "Data/EnemyData/StateData/DetectPlayerStateData")]
public class DetectPlayerStateData : ScriptableObject
{
    public float detectStayTime = 0.3f;
}
