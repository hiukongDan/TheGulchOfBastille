using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private Slider slider;

    void OnEnable()
    {
        slider = GetComponent<Slider>();

        UIEventManager.instance.hpChangeHandler += OnHpChange;
    }

    public void OnHpChange(float current, float total)
    {
        slider.value = current / total;
    }

    void OnDisable()
    {
        UIEventManager.instance.hpChangeHandler -= OnHpChange;
    }
}
