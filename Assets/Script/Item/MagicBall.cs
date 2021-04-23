using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float flyingSpeed = 10f;
    public float destroyDelay = 10f;
    private Animator animator;
    private Rigidbody2D rb2d;
    private bool isHit = false;
    private CombatData combatData;
    private Vector2 direction;

    public void SetCombatData(CombatData combatData) => this.combatData = combatData;
    public void SetDirection(Vector2 direction) => this.direction = direction;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(AnimationHash.MagicBall.Fly);
        
        rb2d = GetComponent<Rigidbody2D>();

        Invoke("Destroy", destroyDelay);

        rb2d.velocity = direction.normalized * flyingSpeed;

        //Debug.Log("Generated magic ball");
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
            if(rb2d == null){
                rb2d = GetComponent<Rigidbody2D>();
            }
            rb2d.velocity = Vector2.zero;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            rb2d.GetComponentInChildren<CircleCollider2D>().enabled = false;
            
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
