using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayState : UIState
{
    public UIPlayState(UIHandler uiHandler, GameObject parentNode) : base(uiHandler, parentNode)
    {
    
    }

    public override void Enter()
    {
        base.Enter();
        
        // On entering Play Menu State, hook handler to this script's function
        // UIEventListener.Instance.pauseMenuHandler += OnEnterPauseMenu;
    }

    public override void Exit()
    {
        // On entering Play Menu State, unhook handler from this script's function
        // UIEventListener.Instance.pauseMenuHandler -= OnEnterPauseMenu;
    }

    public override void Update()
    {
    
    }

    // public void OnEnterPauseMenu()
    // {
    //     uiHandler.uiFSM.PushState(uiHandler.uiPauseState);
    // }

}