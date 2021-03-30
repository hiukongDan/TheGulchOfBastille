using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CombatData
{
    public float damage;
    public float stunDamage;
    public Vector2 position;

    public Vector2 knockbackDir;
    public float knockbackImpulse;

    public GameObject from;
    public int facingDirection;

    public bool isParryDamage;
//    public bool isSimpleDamage;

    // public CombatData(Vector2 position,  Vector2 knockackDir, float damage=0, float stunDamage=0, float knockbackImpulse=0f,
    //      GameObject from=null, int facingDirection=1, bool isParryDamage=false, bool isSimpleDamage=false){
    //          this.position = position;
    //          this.knockbackDir = knockackDir;
    //          this.damage = damage;
    //          this.stunDamage = stunDamage;
    //          this.knockbackImpulse = knockbackImpulse;
    //          this.from = from;
    //          this.facingDirection = facingDirection;
    //          this.isParryDamage = isParryDamage;
    //          this.isSimpleDamage = isSimpleDamage;
    // }

};
