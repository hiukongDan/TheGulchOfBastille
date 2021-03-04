using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIEffect
{
    // Naming Rule: PreNode_SubNode
    Transition_CrossFade,
};

public class UIEffectHandler : MonoBehaviour
{
    void Start()
    {

    }
    
    void Update()
    {
        
    }

    #region PUBLIC INTERFACE
    public void OnPlayUIEffect(UIEffect uiEffect, string animName="start")
    {
        string path = string.Join("/", uiEffect.ToString().Split('~'));
        Animator anim = transform.Find(path)?.GetComponentInChildren<Animator>();
        anim?.Play(animName);
    }

    #endregion
}
