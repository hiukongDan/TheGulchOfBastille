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

        // On entering Pause Menu State, hook handler to this script's function
        // UIEventListener.Instance.pauseMenuHandler += OnExitPauseMenu;
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;

        // On entering Pause Menu State, unhook handler from this script's function
        // UIEventListener.Instance.pauseMenuHandler -= OnExitPauseMenu;
    }

    public override void Update()
    {
        base.Update();
    }

    
    // public void OnExitPauseMenu()
    // {
    //     uiHandler.uiFSM.PopState();
    // }
    

}
