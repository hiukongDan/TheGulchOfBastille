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
		originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	protected Vector2 originPosition;
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			StartCoroutine(bounce());
		}
	}

    IEnumerator bounce(){
		var curve = new Gulch.BouncingCurve_Instance_A(distance, duration);
		float currentOffset = transform.position.y - originPosition.y;
		float time = curve.GetFirstRoot(currentOffset);
		
		while(!curve.IsEnd(time)){
			time += Time.deltaTime;
			transform.position = new Vector2(originPosition.x, 
				originPosition.y + curve.GetValue(time));
			yield return new WaitForEndOfFrame();
		}
		
		transform.position = originPosition;
    }
}
