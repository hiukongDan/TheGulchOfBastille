using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float MaxHp = 100;

    // how many states the barrel has
    public int BrokenStateCount = 1;
    public int barrelTypeNo;
    public ConsumableLoot loot;
    public Vector2 LootAmountRange = new Vector2(1, 100);
    private float currentHp = 0;
    
    private Animator animator;

    void Awake() {
        Reset();
    }

    void Reset(){
        currentHp = MaxHp;
        animator = GetComponentInChildren<Animator>();
        animator.Play(getAnimString("stand"));
    }
    
    void OnEnable() {
        if(currentHp <= 0f){
            animator = GetComponentInChildren<Animator>();
            animator.Play(getAnimString("broke"));
        }
    }

    void Damage(CombatData data){
        if(currentHp <= 0f){
            return;
        }

        animator = GetComponentInChildren<Animator>();
        currentHp -= data.damage;
        if(currentHp <= 0f){
            animator.Play(getAnimString("die"));
            GetComponentInChildren<BoxCollider2D>().enabled = false;
            // TODO: spawn loots
            SpawnLoot();
        }
        else{
            int index = BrokenStateCount - Mathf.CeilToInt(currentHp / (MaxHp / BrokenStateCount));
            if(index >= BrokenStateCount){
                index = BrokenStateCount - 1;
            }
            else if(index < 0){
                index = 0;
            }

            animator.Play(getAnimString("hit", index));
        }
    }

    string getAnimString(string action, int id = -1){
        /* Animation Naming Rule: Barrel_<type No.>[_action][_action No.] */
        string res = "barrel_" + barrelTypeNo;
        res += "_" + action;
        if(id != -1){
            res += "_" + id;
        }
        return res;
    }

    void SpawnLoot(){
        loot.amount = (int)Random.Range(LootAmountRange.x, LootAmountRange.y);
        loot.consumable = (ItemData.Consumable)Mathf.FloorToInt(Random.Range(0, (float)ItemData.Consumable.Count));
        loot.gameObject.SetActive(true);
        overrideGUID();
    }

    void overrideGUID(){
        
        loot.GetComponent<GulchGUID>().ID = "";
    }
}
