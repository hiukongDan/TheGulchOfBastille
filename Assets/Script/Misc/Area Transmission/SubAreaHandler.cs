using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAreaHandler : MonoBehaviour
{
    public SceneCode sceneCode;
    public void OnPerformAction()
    {
        AreaTransmissionHandler.Instance.OnPerformAreaTransmission(sceneCode);
    }
}
