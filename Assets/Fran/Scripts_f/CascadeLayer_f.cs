using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CascadeLayer_f : MonoBehaviour
{
    public float LayerSpeedRelativeToCameraX = 1f;
    public float LayerSpeedRelativeToCameraY = 1f;
	
	public Camera followCamera;
	
    private Vector3 lastCamPos;
    private Vector3 deltaCamPos;
	
    void Start()
    {
        lastCamPos = Camera.main.transform.position;

        transform.position = new Vector3(transform.position.x + LayerSpeedRelativeToCameraX * lastCamPos.x,
            transform.position.y, transform.position.z);
    }

    void Update()
    {
		deltaCamPos = Camera.main.transform.position - lastCamPos;
		lastCamPos = Camera.main.transform.position;
	
		transform.position = new Vector3(transform.position.x + LayerSpeedRelativeToCameraX * deltaCamPos.x,
			transform.position.y + LayerSpeedRelativeToCameraY * deltaCamPos.y, transform.position.z);
	}

}
