using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    [SerializeField]
    private float pushImpulse = 4f;
    public void Damage(CombatData data){
        if(data.from != null && data.from.tag == "Player"){
            Debug.Log("from");
            if(Mathf.Abs(data.position.x - transform.position.x) > 0.3){
                bool isFromLeft = data.position.x - transform.position.x < 0;
                GetComponent<Rigidbody2D>().AddForce(new Vector2((isFromLeft?1:-1)*pushImpulse, 0), ForceMode2D.Impulse);
            }
        }
    }
}
