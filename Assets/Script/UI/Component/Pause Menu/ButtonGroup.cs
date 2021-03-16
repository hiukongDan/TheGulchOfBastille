using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ButtonGroup : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Button> buttons = null;
    private GameManager gm;

    public Button selectedButton = null;

    public bool IsUsingInvokeToAutoSelect = true;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void OnEnable()
    {
        if(buttons == null)
        {
            buttons = new List<Button>();
        }
        buttons.Clear();
        foreach(Button button in GetComponentsInChildren<Button>()){
            buttons.Add(button);
        }

        // Debug.Log(buttons.Count);
        // EventSystem.current.firstSelectedGameObject = buttons[0].gameObject;
        if(IsUsingInvokeToAutoSelect){
            Invoke("Select", Time.deltaTime);
        }
        else{
            selectedButton = buttons[0];
            selectedButton.Select();
        }
    }

    void OnDisable() {
        selectedButton = null;    
    }

    private void Select(){
        OnSelect(buttons[0]);
    }

    public void OnSelect(Button button){
        if(selectedButton == null || selectedButton != button){
            selectedButton = button;
        }
        selectedButton.Select();
        Debug.Log("catch");
    }

    public void OnClick(){
        UIState.UIStateEventData data;
        data.index = GetIndexOfCurrentSelected();
        gm.uiHandler.uiFSM.PeekState()?.OnClick(data);
    }

    public void EnableButton(int index){
        if(index >= 0 && index < buttons.Count){
            buttons[index].interactable = true;
        }
    }

    public void EnableButton(Button button){
        button.interactable = true;
    }

    public void DisableButton(int index){
        if(index >= 0 && index < buttons.Count){
            buttons[index].interactable = false;
        }
    }

    public void DisableButton(Button button){
        button.interactable = false;
    }

    public int GetIndexOfCurrentSelected() => buttons.IndexOf(selectedButton);


}
