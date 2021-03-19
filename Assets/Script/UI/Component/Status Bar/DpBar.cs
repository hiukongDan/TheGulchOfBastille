using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DpBar : StatusBar
{
    protected override void OnEnable() {

        base.OnEnable();

        UIEventListener.Instance.dpChangeHandler += OnBarValueChange;
    }

    protected override void OnDisable() {
        base.OnDisable();
        
        UIEventListener.Instance.dpChangeHandler -= OnBarValueChange;
    }
}
