using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public float ArrowVolocity = 30f;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * ArrowVolocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.zero;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        if(other.gameObject.tag == "Enemy"){
            Debug.Log("Enemy");
            transform.parent.parent = other.gameObject.transform;
            GameObject.Destroy(transform.parent.gameObject);
        }
        else{
            
        }
    }

    
}
