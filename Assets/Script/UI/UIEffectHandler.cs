using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIEffect
{
    // Naming Rule: PreNode_SubNode
    Transition_CrossFade,
    Transition_CrossFadeWhite,
};

public enum UIEffectAnimationClip
{
    // aliases in animator tab
    start, end, empty, dark, white,
};

public class UIEffectHandler : MonoBehaviour
{
    #region PUBLIC INTERFACE

    /// <returns>
    /// The length of the effect animation clip
    /// </returns>
    public float OnPlayUIEffect(UIEffect uiEffect, UIEffectAnimationClip clipName)
    {
        string path = string.Join("/", uiEffect.ToString().Split('_'));
        Animator anim = transform.Find(path)?.GetComponentInChildren<Animator>();
        anim?.Play(clipName.ToString());
        return anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    #endregion
}
