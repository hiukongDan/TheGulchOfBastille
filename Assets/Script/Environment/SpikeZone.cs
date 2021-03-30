using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeZone : MonoBehaviour
{
   private float timer = 0;
   public float applyDamageTimerMax = 1f;
    public Vector2 applyImpulseDir = new Vector2(0f, 10f);
    public float applyImpulse = 10f;
    public float damage = 10f;

    private bool isStartApplyDamage = false;

    private CombatData damageData;
    private Player playerCache;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            isStartApplyDamage = true;
            timer = -1f;
            playerCache = other.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        isStartApplyDamage = false;
    }

    void Update(){
        if(isStartApplyDamage){
            if(timer >= 0){
                timer -= Time.deltaTime;
            }
            else{
                timer = applyDamageTimerMax;
                // playerCache?.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(applyImpulseDir.x/2, applyImpulseDir.x),
                //     Random.Range(applyImpulseDir.y/2, applyImpulseDir.y)), ForceMode2D.Impulse);
                playerCache?.gameObject.SendMessage("Damage", damageData);

                timer = applyDamageTimerMax;
            }
        }

    }

    void OnEnable(){
        damageData = new CombatData();
        damageData.damage = damage;
        damageData.facingDirection = -1;
        damageData.from = gameObject;
        damageData.isParryDamage = false;
        damageData.knockbackDir = applyImpulseDir;
        damageData.knockbackImpulse = applyImpulse;
        damageData.knockbackDir = Vector2.zero;
        damageData.knockbackDir = Vector2.zero;
        damageData.position = transform.position;
        damageData.stunDamage = 0f;
    }
}
