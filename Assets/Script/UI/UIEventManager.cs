using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    public static UIEventManager instance;

    public UIEventManager()
    {
        instance = this;
    }

    public delegate void HpChangeHandler(float current, float total);

    public event HpChangeHandler hpChangeHandler;

    public void OnHpChange(float current, float total)
    {
        hpChangeHandler?.Invoke(current, total);
    }
}
