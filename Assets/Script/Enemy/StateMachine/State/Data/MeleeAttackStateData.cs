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

    public float cooldownTimer = 1f;

    // attack + additional attack
    public int heideAttackTimes = 4;

    public CombatData GetCombatData(){
        CombatData combatData = new CombatData();
        combatData.damage = damage;
        combatData.stunDamage = stunDamage;
        combatData.knockbackDir = knockbackDir;
        combatData.knockbackImpulse = knockbackImpulse;

        return combatData;
    }
}
