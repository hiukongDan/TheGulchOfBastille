using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFiniteStateMachine
{
    private Stack<UIState> uiStateStack;

    public UIFiniteStateMachine()
    {
        uiStateStack = new Stack<UIState>();
    }

    public void InitStateMachine(UIState initState)
    {
        uiStateStack.Clear();
        uiStateStack.Push(initState);
        uiStateStack.Peek().Enter();
    }

    public void SwtichState(UIState newState)
    {
        uiStateStack.Pop().Exit();
        uiStateStack.Push(newState);
        newState.Enter();
    }
    
    public void PushState(UIState newState)
    {
        uiStateStack.Peek().Exit();
        uiStateStack.Push(newState);
        newState.Enter();
    }

    public UIState PopState()
    {
        if (uiStateStack.Count == 0)
            return null;

        UIState uiState = uiStateStack.Pop();
        uiState.Exit();
        uiStateStack.Peek().Enter();
        return uiState;
    }

    public UIState PeekState()
    {
        return uiStateStack.Peek();
    }

    public void Update()
    {
        uiStateStack.Peek()?.Update();
    }

    /// <summary>
    /// Return current size of uiStack
    /// </summary>
    public int Count(){
        return uiStateStack.Count;
    }
}
