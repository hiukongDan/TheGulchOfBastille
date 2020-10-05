using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToAlive : MonoBehaviour
{
    public MeleeAttackState meleeAttackState;
    private Entity entity;

    void Start()
    {
        entity = transform.parent.GetComponent<Entity>();
    }

    public void Damage(CombatData combatData)
    {
        entity.SendMessage("Damage", combatData);
    }

    public void DoMeleeAttack()
    {
        if(meleeAttackState != null)
            meleeAttackState.DoMeleeAttack();
    }

    public void CompleteMeleeAttack()
    {
        if(meleeAttackState != null)
            meleeAttackState.CompleteMeleeAttack();
    }
}
