using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackActor : MonoBehaviour
{
    public Vector2 knockbackDir = new Vector2(1, 1);
    public float knockbackImpulse = 0.5f;
    public float knockbackTorque = 1f;

    public bool isApplyForce = true;
    public bool isApplyTorque = true;

    private Rigidbody2D rb2d;
    public void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        knockbackDir.Normalize();
    }

    public void Damage(CombatData combatData)
    {
        if(rb2d == null)
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        if(rb2d == null)
        {
            return;
        }

        if(combatData.facingDirection >= 0)
        {
            if(isApplyForce)
                rb2d.AddForce(knockbackDir * knockbackImpulse, ForceMode2D.Impulse);
            if(isApplyTorque)
                rb2d.AddTorque(-knockbackTorque, ForceMode2D.Impulse);
        }
        else
        {
            if(isApplyForce)
                rb2d.AddForce(new Vector2(-knockbackDir.x * knockbackImpulse, knockbackDir.y * knockbackImpulse), ForceMode2D.Impulse);
            if(isApplyTorque)
                rb2d.AddTorque(knockbackTorque, ForceMode2D.Impulse);
        }
    }
}
