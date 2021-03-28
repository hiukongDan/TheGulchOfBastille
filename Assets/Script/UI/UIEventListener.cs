using System;
using UnityEngine.Events;
using UnityEngine;

public class UIEventListener
{
    private static UIEventListener _instance;

    public static UIEventListener Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new UIEventListener();
            }
            return _instance;
        }
    }

    #region DELEGATE
    public delegate void StatusBarChangeHandler(float current, float total);
    public delegate void SimpleEventHandler();
    public delegate void ValueChangeHandler(float value);
    public delegate void InfomationChangeHandler(InfomationChangeData data);
    #endregion

    #region EVENTS
    public event StatusBarChangeHandler hpChangeHandler;
    public event StatusBarChangeHandler dpChangeHandler;
    public event SimpleEventHandler fullscreenSwitchHandler;
    public event ValueChangeHandler uilosChangeHandler;
    public event InfomationChangeHandler infomationChangeHandler;
    // public event Action pauseMenuHandler;
    #endregion

    #region EVENT STRUCT
    public struct InfomationChangeData{
        public enum DisplayType{
            Normal, WipeUp, WipeDown, SlideLeft, SlideRight,
        }

        public string info;
        public DisplayType displayType;
        public InfomationChangeData(string info, DisplayType displayType = DisplayType.Normal){
            this.info = info;
            this.displayType = displayType;
        }
    };
    #endregion

    #region INTERFACE TO OUTER SCRIPTS
    public void OnHpChange(float current, float total)
    {
        hpChangeHandler?.Invoke(current, total);
        //Debug.Log("hp");
    }

    public void OnDpChange(float current, float total)
    {
        dpChangeHandler?.Invoke(current, total);
    }

    public void OnFullscreenSwitch(){
        fullscreenSwitchHandler.Invoke();
    }

    public void OnUilosChange(float value){
        uilosChangeHandler.Invoke(value);
    }

    public void OnInfomationChange(InfomationChangeData data){
        infomationChangeHandler?.Invoke(data);
    }

    // public void OnPauseMenu()
    // {
    //     pauseMenuHandler?.Invoke();
    // }
    #endregion
}
