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

    private enum animState { EMPTY, ATTACK, IDLE};

    private animState currentAnimState;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentAnimState = animState.EMPTY;
    }

    public void BeginAttack()
    {
        if(currentAnimState != animState.ATTACK)
        {
            anim.Play("attack");
            currentAnimState = animState.ATTACK;
        }
    }

    public void Hide()
    {
        if (currentAnimState != animState.EMPTY)
        {
            anim.Play("empty");
            currentAnimState = animState.EMPTY;
        }
    }

    public void Idle()
    {
        if (currentAnimState != animState.IDLE)
        {
            anim.Play("idle");
            currentAnimState = animState.IDLE;
        }
    }

    public void CompleteTentacleAttack()
    {
        tentacleAttackStage?.CompleteTentacleAttack(index);

        currentAnimState = animState.IDLE;
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
