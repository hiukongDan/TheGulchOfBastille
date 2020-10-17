using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    protected Slider slider;

    protected virtual void OnEnable()
    {
        slider = GetComponent<Slider>();
    }

    protected void OnBarValueChange(float current, float total)
    {
        slider.value = current / total;
    }

    protected virtual void OnDisable()
    {

    }
}
