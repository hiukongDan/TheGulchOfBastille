using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    public void CompleteHit()
    {
        Destroy(gameObject);
    }
}
