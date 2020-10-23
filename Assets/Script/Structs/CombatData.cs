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

    public bool isParryDamage;
};
