using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/EnemyData/StateData/IdleStateData")]
public class IdleStateData : ScriptableObject
{
    public float idleTimeMin = 1f;
    public float idleTimeMax = 4f;
}
