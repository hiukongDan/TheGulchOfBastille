using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public float ArrowVelocity = 30f;
    public CombatData combatData;

    public void SetCombatData(CombatData combatData) => this.combatData = combatData;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = ArrowVelocity * transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.zero;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        rb2d.GetComponentInChildren<CircleCollider2D>().enabled = false;

        if(other.gameObject.tag == "Enemy"){
            // Debug.Log("Enemy");
            transform.parent.parent = other.gameObject.transform;
            Player player = GameObject.Find("Player").GetComponent<Player>();
            other.gameObject.SendMessage("Damage", combatData);
            if(other.gameObject.GetComponentInParent<Entity>() != null && !other.gameObject.GetComponentInParent<Entity>().IsDead()){
                GameObject.Destroy(transform.parent.gameObject);
            }
        }
    }
    
}
