using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseState : UIState
{
    public UIPauseState(UIHandler uiHandler, GameObject parentNode): base(uiHandler, parentNode)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        UIEventListener.Instance.pauseMenuHandler += OnExitPauseMenu;
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;
        UIEventListener.Instance.pauseMenuHandler -= OnExitPauseMenu;
    }


    public override void Update()
    {
        
    }

    public void OnExitPauseMenu()
    {

        uiHandler.uiFSM.PopState();
    }

}
