using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerClickHandler
{
    private ButtonGroup buttonGroup;
    private Button button;
    void Awake(){
        buttonGroup = GetComponentInParent<ButtonGroup>();
        button = GetComponent<Button>();
    }

    void ISelectHandler.OnSelect(BaseEventData eventData){
        buttonGroup.OnSelect(button);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData){
        if(button.interactable){
            buttonGroup.OnSelect(button);
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData){
        // Debug.Log("hello");
        buttonGroup.OnClick();
    }

    
}
