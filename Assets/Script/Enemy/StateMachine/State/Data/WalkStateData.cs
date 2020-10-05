using UnityEngine;

[CreateAssetMenu(fileName = "newWalkStateData", menuName = "Data/EnemyData/StateData/WalkStateData")]
public class WalkStateData : ScriptableObject
{
    public float walkSpeed = 3f;
    public float walkTimeMax = 10f;
    public float walkTimeMin = 5f;
}
