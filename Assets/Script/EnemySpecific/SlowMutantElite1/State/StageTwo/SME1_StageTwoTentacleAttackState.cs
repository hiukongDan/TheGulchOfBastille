using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SME1_StageTwoTentacleAttackState : State
{
    protected float cooldownTimer;
    protected MeleeAttackStateData data;
    protected SlowMutantElite1 enemy;

    protected List<int> attackTentacles;

    protected bool isTentacleAttack;

    protected CombatData combatData;

    public SME1_StageTwoTentacleAttackState(FiniteStateMachine stateMachine, Entity entity, string animName, MeleeAttackStateData tentacleStateData, SlowMutantElite1 enemy) : base(stateMachine, entity, animName)
    {
        cooldownTimer = -1f;
        this.data = tentacleStateData;
        this.enemy = enemy;
        attackTentacles = new List<int>();

        combatData = new CombatData();
        combatData.damage = data.damage;
        combatData.stunDamage = data.stunDamage;
        combatData.knockbackDir = data.knockbackDir;
        combatData.knockbackImpulse = data.knockbackImpulse;
        combatData.stunDamage = data.stunDamage;
        combatData.from = enemy.aliveGO;
        combatData.isParryDamage = false;

        for(int i = 0; i < 4; i++)
        {
            enemy.SnakeHeads[i].tentacleAttackStage = this;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public void CompleteTentacleAttack(int index)
    {
        attackTentacles.Remove(index);

        if (attackTentacles.Count <= 0)
            isTentacleAttack = false;
    }

    public void CompleteTentacleAttack()
    {
        ResetTimer();
        stateMachine.SwitchState(enemy.stageTwoIdleState);
    }

    public void CheckDamageBox(int index, Vector2 attackCenter, Vector2 damageBox)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackCenter, damageBox, 0, data.whatIsPlayer);
        combatData.position = enemy.aliveGO.transform.position;
        combatData.facingDirection = index < 2 ? -1 : 1;
        foreach(var collider in colliders)
        {
            if(collider.tag == "Player")
            {
                collider.gameObject.SendMessage("Damage", combatData);
            }
        }
    }

    public override void Enter()
    {
        base.Enter();

        isTentacleAttack = true;

        GenerateAttackTentacles();

        foreach(int index in attackTentacles)
        {
            enemy.SnakeHeads[index].BeginAttack();
        }
    }

    protected void GenerateAttackTentacles()
    {
        attackTentacles.Clear();

        int rand = Mathf.FloorToInt(Random.value * 5);
        switch (rand)
        {
            case 0:
                // mode 0 = 2,3
                attackTentacles.Add(2);
                attackTentacles.Add(3);
                break;
            case 1:
                // mode 1 = 0,1
                attackTentacles.Add(0);
                attackTentacles.Add(1);
                break;
            case 2:
                // mode 2 = 0, 2
                attackTentacles.Add(0);
                attackTentacles.Add(2);
                break;
            case 3:
                // mode 3 = 1, 3
                attackTentacles.Add(1);
                attackTentacles.Add(3);
                break;
            case 4:
                // mode 4 = 0, 1, 2, 3
                attackTentacles.Add(0);
                attackTentacles.Add(1);
                attackTentacles.Add(2);
                attackTentacles.Add(3);
                break;
            default:
                // default mode 0
                attackTentacles.Add(2);
                attackTentacles.Add(3);
                break;
        }


/*        if (rand < oneTentacleWeight)
        {
            attackTentacles.Add(Mathf.FloorToInt(Random.value * 4));
        }
        else if (rand < oneTentacleWeight + twoTentacleWeigth)
        {
            while (attackTentacles.Count < 2)
            {
                int val = Mathf.FloorToInt(Random.value * 4);
                if (!attackTentacles.Contains(val))
                {
                    attackTentacles.Add(val);
                }
            }
        }
        else if (rand < oneTentacleWeight + twoTentacleWeigth + threeTentacleWeight)
        {
            for (int i = 0; i < 4; i++)
            {
                attackTentacles.Add(i);
            }
            attackTentacles.Remove(Mathf.FloorToInt(Random.value * 4));
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                attackTentacles.Add(i);
            }
        }*/
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isTentacleAttack == false)
        {
            stateMachine.SwitchState(enemy.stageTwoIdleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ResetTimer()
    {
        cooldownTimer = data.cooldownTimer;
    }

    public override void UpdateTimer()
    {
        if(cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public override bool CanAction()
    {
        return cooldownTimer < 0;
    }
}
