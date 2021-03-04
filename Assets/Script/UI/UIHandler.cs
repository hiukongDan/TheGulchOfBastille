using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public UIFiniteStateMachine uiFSM { get; private set; }

    public UIEffectHandler uiEffectHandler;

    public GameObject uiPlayGO;
    public GameObject uiPauseGO;

    public UIPlayState uiPlayState { get; private set; }
    public UIPauseState uiPauseState { get; private set; }

    void Awake()
    {
        uiFSM = new UIFiniteStateMachine();
    }

    void OnEnable()
    {

    }

    void Start()
    {
        uiPauseState = new UIPauseState(this, uiPauseGO);
        uiPlayState = new UIPlayState(this, uiPlayGO);

        uiFSM.InitStateMachine(uiPlayState);
    }

    void Update()
    {
        uiFSM.Update();
    }
}
