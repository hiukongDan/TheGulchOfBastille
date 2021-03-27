using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UilosDisplay : MonoBehaviour
{
    private PFontText pFontText;

    public int maxDigit = 9;

    void Awake(){
        pFontText = GetComponentInChildren<PFontText>();
    }

    void OnEnable() {
        UIEventListener.Instance.uilosChangeHandler += OnUilosAmountChange;
    }

    void OnDisable() {
        UIEventListener.Instance.uilosChangeHandler -= OnUilosAmountChange;
    }

    void OnUilosAmountChange(float value){
        pFontText.SetText(parseNum(((int)value).ToString()));
        // Debug.Log("recieve");
    }

    private string parseNum(string num){
        string res = "";
        if(num.Length < maxDigit){
            
            res = new string('0', maxDigit-num.Length);
        }
        res = "*" + res + num;

        return res;
    }


}
