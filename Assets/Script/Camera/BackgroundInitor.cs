using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInitor : MonoBehaviour
{
    public Transform backgroundImg;

    void Start()
    {
        backgroundImg.position = new Vector3(Camera.main.transform.position.x, backgroundImg.position.y, backgroundImg.position.z);
    }
}
