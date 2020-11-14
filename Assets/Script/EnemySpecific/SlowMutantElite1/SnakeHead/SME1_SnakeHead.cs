using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_SnakeHead : MonoBehaviour
{
    private Animator anim;

    public Transform damageBox;
    public Vector2 damageBoxSize;

    public bool IsDrawGizmos;

    public SME1_StageTwoTentacleAttackState tentacleAttackStage;

    public int index;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void BeginAttack()
    {
        anim.Play("attack");
    }

    public void CompleteTentacleAttack()
    {
        tentacleAttackStage?.CompleteTentacleAttack();
    }

    public void CheckDamagebox()
    {
        tentacleAttackStage?.CheckDamageBox(index, damageBox.position, damageBoxSize);
    }

    private void OnDrawGizmos()
    {
        if (IsDrawGizmos)
        {
            Gizmos.DrawWireCube(damageBox.position, damageBoxSize);
        }
    }

}
