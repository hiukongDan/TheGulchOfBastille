using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummy : MonoBehaviour
{
    public int maxHealth;
    public float strikeForce;
    public float knockBackForce;
    public float knockBackTorque;
    public float brokenPartLingerTime;

    private int currentHealth;

    public GameObject hitParticlePref;

    public GameObject brokenBotGO, brokenTopGO;

    private Rigidbody2D rb, brokenBotRb, brokenTopRb;

    private Animator anim;

    private Player player;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        brokenBotRb = brokenBotGO.GetComponent<Rigidbody2D>();
        brokenTopRb = brokenTopGO.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Damage(CombatData data)
    {
        currentHealth -= (int)data.damage;
        if(currentHealth <= 0)
        {
            brokenBotGO.SetActive(true);
            brokenTopGO.SetActive(true);

            gameObject.SetActive(false);

            brokenTopRb.transform.position = transform.position;
            brokenBotRb.transform.position = transform.position;

            Vector2 newForce = Vector2.zero;
            if (player.transform.position.x - transform.position.x >= 0)
                newForce.x = knockBackForce * -1;
            else
                newForce.x = knockBackForce;

            brokenBotRb.AddForce(newForce, ForceMode2D.Impulse);

            brokenTopRb.AddTorque(player.facingDirection * knockBackTorque);

            Destroy(brokenTopGO.transform.parent.gameObject, brokenPartLingerTime);
        }
        else
        {
            if(player.transform.position.x - transform.position.x >= 0)
            {
                anim.SetBool("isStrikeRight", true);
                rb.AddForce(new Vector2(strikeForce * -1, 0), ForceMode2D.Impulse);
            }
            else
            {
                anim.SetBool("isStrikeRight", false);
                rb.AddForce(new Vector2(strikeForce, 0), ForceMode2D.Impulse);
            }

            anim.SetBool("isStriking", true);

            var particle = Instantiate(hitParticlePref);
            particle.transform.position = GameObject.Find("Player/attack1hitbox").transform.position;
            particle.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
    }

    public void CompleteStrike()
    {
        anim.SetBool("isStriking", false);
    }
}
