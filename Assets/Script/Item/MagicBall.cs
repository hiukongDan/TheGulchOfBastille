using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float flyingSpeed = 10f;
    public float destroyDelay = 10f;
    private Animator animator;
    private Rigidbody2D rb2d;
    private bool isHit;
    private CombatData combatData;

    public void SetCombatData(CombatData combatData) => this.combatData = combatData;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(AnimationHash.MagicBall.Fly);
        
        rb2d = GetComponent<Rigidbody2D>();
        isHit = false;

        Invoke("Destroy", destroyDelay);
    }

    void Update()
    {
        
    }

    void Destroy(){
        GameObject.Destroy(transform.parent.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!isHit){
            isHit = true;
            rb2d.velocity = Vector2.zero;
            StartCoroutine(TriggerHitAnimation());
            if(other.gameObject.tag == "Enemy"){
                other.gameObject.SendMessage("Damage", combatData);
            }
        }
    }
    IEnumerator TriggerHitAnimation(){
        animator.Play(AnimationHash.MagicBall.Hit);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(transform.parent.gameObject);
    }
}
