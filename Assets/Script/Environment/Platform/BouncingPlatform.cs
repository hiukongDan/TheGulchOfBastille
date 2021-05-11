using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatform : MonoBehaviour
{
	public float distance = 1.2f;
	public float duration = 4f;
	
    // Start is called before the first frame update
    void Start()
    {
        isBouncingMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	protected bool isBouncingMovement;

	/*
    void OnCollisionEnter2D(Collision2D collision) {
		BoxCollider2D selfCollider = transform.GetComponent<BoxCollider2D>();
		if(!isBouncingMovement && Gulch.Math.AlmostGreater(collision.GetContact(0).point.y, 
							transform.position.y + selfCollider.size.y/2)){
			isBouncingMovement = true;
			StartCoroutine(bounce());
		}
    }
	*/
	
	void OnTriggerEnter2D(Collider2D other){
		if(!isBouncingMovement){
			isBouncingMovement = true;
			StartCoroutine(bounce());
		}
	}

    IEnumerator bounce(){
		var curve = new Gulch.BouncingCurve_Instance_A(distance, duration);
		float time = 0f;
		Vector2 originPosition = transform.position;
		while(!curve.IsEnd(time)){
			time += Time.deltaTime;
			transform.position = new Vector2(originPosition.x, 
				originPosition.y + curve.GetValue(time));
			yield return new WaitForEndOfFrame();
		}
		
		isBouncingMovement = false;
    }
}
