using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSwitchListener : MonoBehaviour
{
    // Start is called before the first frame update
    bool lastFullscreen;
    void Start()
    {
        lastFullscreen = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        if(Screen.fullScreen != lastFullscreen){
            lastFullscreen = Screen.fullScreen;
            UIEventListener.Instance.OnFullscreenSwitch();
        }
    }
}
