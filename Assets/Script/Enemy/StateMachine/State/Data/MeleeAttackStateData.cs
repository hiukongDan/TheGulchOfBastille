using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/EnemyData/StateData/MeleeAttackStateData")]
public class MeleeAttackStateData : ScriptableObject
{
    public int damage = 10;
    public int stunDamage = 1;
    public Vector2 knockbackDir = new Vector2(1, 1);
    public float knockbackImpulse = 5f;
    public float attackRadius = 1f;
    public LayerMask whatIsPlayer;

    // attack + additional attack
    public int heideAttackTimes = 4;
}
