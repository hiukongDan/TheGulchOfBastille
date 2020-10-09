using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
