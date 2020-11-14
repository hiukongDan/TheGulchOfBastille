using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraBackgroundResize : MonoBehaviour
{
    public Vector2 defaultResolution = new Vector2(640, 448);
    private Vector3 defaultScales;

    void Start()
    {
        defaultScales = transform.localScale;
        Vector3 tmp = new Vector3(Screen.currentResolution.width / defaultResolution.x * defaultScales.x, Screen.currentResolution.height / defaultResolution.y * defaultScales.y, defaultScales.z);
        transform.localScale = tmp;
    }
}
