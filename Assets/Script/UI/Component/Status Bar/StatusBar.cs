using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    protected Slider slider;

    public Vector2 DeltaSizeMultiplier = new Vector2(1,1);
    protected Vector2 oldSizeDelta;
    void Start(){
        
    }

    protected virtual void OnEnable()
    {
        slider = GetComponent<Slider>();
        oldSizeDelta = slider.GetComponent<RectTransform>().sizeDelta;
    }

    protected void OnBarValueChange(float current, float total)
    {
        slider.value = current / total;
        slider.GetComponent<RectTransform>().sizeDelta = new Vector2(DeltaSizeMultiplier.x * total, DeltaSizeMultiplier.y * oldSizeDelta.y);
    }

    protected virtual void OnDisable()
    {

    }
}
