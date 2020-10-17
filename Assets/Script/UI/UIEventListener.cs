
using System;

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

    public delegate void StatusBarChangeHandler(float current, float total);

    public event StatusBarChangeHandler hpChangeHandler;
    public event StatusBarChangeHandler dpChangeHandler;
    public event Action pauseMenuHandler;

    public void OnHpChange(float current, float total)
    {
        hpChangeHandler?.Invoke(current, total);
    }

    public void OnDpChange(float current, float total)
    {
        dpChangeHandler?.Invoke(current, total);
    }

    public void OnPauseMenu()
    {
        pauseMenuHandler?.Invoke();
    }

}
