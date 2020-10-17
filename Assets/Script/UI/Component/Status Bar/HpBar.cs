using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : StatusBar
{

    protected override void OnEnable()
    {
        base.OnEnable();

        UIEventListener.Instance.hpChangeHandler += OnBarValueChange;
    }

    protected override void OnDisable()
    {
        UIEventListener.Instance.hpChangeHandler -= OnBarValueChange;
    }
}
