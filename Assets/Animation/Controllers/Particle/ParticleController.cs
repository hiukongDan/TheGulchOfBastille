using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    public string clipName;
    void Start()
    {
        GetComponent<Animator>().Play(clipName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
