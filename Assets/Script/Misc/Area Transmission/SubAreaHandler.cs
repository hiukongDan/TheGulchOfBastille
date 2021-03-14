using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAreaHandler : MonoBehaviour
{
    public SceneCode currentSceneCode;
    public SceneCode transitionSceneCode;
    public UIEffect uIEffect;

    public Transform targetSceneInitPos;

    public bool IsTransitionAutomatically = false;
    public void OnPerformAction()
    {
        AreaTransmissionHandler.Instance.OnPerformAreaTransmission(this);
    }
}
