using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIView : MonoBehaviour, IPointerClickHandler
{
    public PFontText text{get; private set;}
    public GameObject chosen{get; private set;}
    public Image image;
    private ViewGroup viewGroup;

    void Awake(){
        text = transform.GetComponentInChildren<PFontText>();
        chosen = transform.Find("chosen").gameObject;
        image = GetComponent<Image>();
        viewGroup = GetComponentInParent<ViewGroup>();
    }

    void OnEnable() {
        ClearView();
    }

    public void OnPointerClick(PointerEventData data){
        viewGroup.OnClick(this);
    }

    public void ClearView(){
        chosen.gameObject?.SetActive(false);
        text.gameObject?.SetActive(false);
        image.sprite = UIIconLoader.EmptySprite;
    }

    public void ClearChosen(){
        chosen.gameObject?.SetActive(false);
    }

}
