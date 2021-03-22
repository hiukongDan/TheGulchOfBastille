using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGroup : MonoBehaviour
{
    public List<UIView> views;
    void OnEnable() {
        if(views == null){
            views = new List<UIView>();
        }
        else{
            views.Clear();
        }

        foreach(UIView view in transform.GetComponentsInChildren<UIView>()){
            views.Add(view);
        }
    }

    public void OnClick(UIView view){
        int index = GetIndexOfView(view);

        
    }

    int GetIndexOfView(UIView view) => views.IndexOf(view);
}
