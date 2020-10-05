using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/EnemyData/EntityData")]
public class EntityData : ScriptableObject
{
    public float wallCheckDistance = 0.3f;
    public float edgeCheckDistance = 0.3f;
    public float groundCheckDistance = 0.5f;

    public float detectPlayerAgroMinDistance = 3f;
    public float detectPlayerAgroMaxDistance = 4f;

    public float meleeAttackDistance = 0.8f;

    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;

    public float damage = 5f;
    public float stunDamage = 1f;

    public float damageBoxWidth = 2;
    public float damageBoxHeight = 1;

    public float maxHealth = 50f;
    public float stunResistance = 3f;

    public float knockbackImpulse = 5f;
    public Vector2 knockbackDir = new Vector2(1,1);


}
