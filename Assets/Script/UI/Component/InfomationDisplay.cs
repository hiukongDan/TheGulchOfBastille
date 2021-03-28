using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfomationDisplay : MonoBehaviour
{
    public float NormalDisplayTime = 3f;

    protected PFontText text;
    void Start()
    {
        text = GetComponentInChildren<PFontText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable() {
        UIEventListener.Instance.infomationChangeHandler += OnInfomationChange;
    }

    void OnDisable() {
        UIEventListener.Instance.infomationChangeHandler -= OnInfomationChange;
        text?.SetText("");
    }

    void OnInfomationChange(UIEventListener.InfomationChangeData data){
        processData(data);
    }

    void processData(UIEventListener.InfomationChangeData data){
        switch(data.displayType){
            case UIEventListener.InfomationChangeData.DisplayType.Normal:
            StopAllCoroutines();
            StartCoroutine(displayNormal(data.info));
            break;
            case UIEventListener.InfomationChangeData.DisplayType.WipeUp:
            break;
            case UIEventListener.InfomationChangeData.DisplayType.WipeDown:
            break;
            case UIEventListener.InfomationChangeData.DisplayType.SlideLeft:
            break;
            case UIEventListener.InfomationChangeData.DisplayType.SlideRight:
            break;
            default:
            text.SetText("");
            break;
        }
    }

    IEnumerator displayNormal(string info){
        text?.SetText(info);
        yield return new WaitForSeconds(NormalDisplayTime);
        text?.SetText("");
    }
}
