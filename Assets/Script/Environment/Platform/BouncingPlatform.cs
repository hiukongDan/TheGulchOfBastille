using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        //StartCoroutine(bounce());    
        Debug.Log("contact count: " + collision.contactCount);
        Debug.Log(collision.GetContact(0).point);
    }

    IEnumerator bounce(){
        Debug.Log("Bounce at " + Time.fixedTime);
        yield return null;
    }
}
