using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IUITab{

}

public class UITab : MonoBehaviour, IUITab, IPointerClickHandler
{
    private TabGroup tabGroup;

    void Awake(){
        tabGroup = GetComponentInParent<TabGroup>();
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData){
        tabGroup.SelectTab(this);
        tabGroup.OnClick();
    }
}
