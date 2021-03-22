using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class CascadeLayer : MonoBehaviour
{
    public float LayerSpeedRelativeToCameraX = 1f;
    public float LayerSpeedRelativeToCameraY = 1f;
	
	public Camera followCamera;
	
    private Vector3 lastCamPos;
    private Vector3 deltaCamPos;
	
	void Awake()
	{
		if(followCamera == null){
			followCamera = Camera.main;
		}
	}

    void Start()
    {
        lastCamPos = followCamera.transform.position;
    }

    void Update()
    {
        deltaCamPos = followCamera.transform.position - lastCamPos;
        lastCamPos = followCamera.transform.position;
        
        transform.position = new Vector3(transform.position.x + LayerSpeedRelativeToCameraX * deltaCamPos.x,
            transform.position.y + LayerSpeedRelativeToCameraY * deltaCamPos.y, transform.position.z);
    }
}
