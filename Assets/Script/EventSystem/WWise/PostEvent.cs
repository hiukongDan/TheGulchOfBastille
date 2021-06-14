using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gulch{
public class PostEvent : MonoBehaviour
{
    public string eventName;
    public bool triggerOnEnable = false;
    void OnEnable() {
        if(triggerOnEnable){
            postEvent(eventName);
        }
    }
    public void postEvent(string eventName){
        var gm = GameObject.Find("GameManager");
        AkSoundEngine.PostEvent(eventName, gm?gm:null);
    }
    
};

};
