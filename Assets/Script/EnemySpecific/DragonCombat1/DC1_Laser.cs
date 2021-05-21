using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC1_Laser : MonoBehaviour
{
    public GameObject beamDustPref;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public float groundDetectLength = 4f;
    private Animator animator;
    private CombatData combatData;
    private Vector2 lastGroundHitPos;
    private float lastDustInstatiateTime;
    private float dustSpawnInterval = 0.02f;

    public void SetCombatData(CombatData combatData) => this.combatData = combatData;

    private void RequireAnimator(){
        if(animator == null){
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Start(){
        RequireAnimator();
        lastGroundHitPos = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        lastDustInstatiateTime = -1f;
    }
    public void InitiateLaser(){
        RequireAnimator();
        animator.Play("laser_initiating");
    }

    public void ShowLaser(){
        gameObject.SetActive(true);
    }
    public void HideLaser(){
        gameObject.SetActive(false);
    }

    void Update() {
        CheckGroundHit();
        CheckPlayerHit();
    }

    void CheckGroundHit(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, groundDetectLength,whatIsGround);
        if(hit.collider != null){
            Vector2 tmp = hit.point;
            // if(!Gulch.Math.AlmostEqual(tmp, lastGroundHitPos, 0.1f)){
            //     lastGroundHitPos = tmp;
            //     GameObject.Instantiate(beamDustPref, hit.point, Quaternion.identity, transform.parent);
            // }
            if(lastDustInstatiateTime < 0){
                GameObject.Instantiate(beamDustPref, hit.point, Quaternion.identity, transform.parent);
                lastDustInstatiateTime = dustSpawnInterval;
            }
            else{
                lastDustInstatiateTime -= Time.deltaTime;
            }
        }
    }

    void CheckPlayerHit(){
        float angle = Vector2.SignedAngle(transform.up, Vector2.up);
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        Collider2D collider = Physics2D.OverlapBox(transform.position - transform.up * groundDetectLength/2,
                                                   new Vector2(sr.sprite.rect.width/sr.sprite.pixelsPerUnit, groundDetectLength),
                                                   angle,
                                                   whatIsPlayer);
        if(collider != null){
            collider.gameObject.SendMessage("Damage", combatData);
        }
    }
}
