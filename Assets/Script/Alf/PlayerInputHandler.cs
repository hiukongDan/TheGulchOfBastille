using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public bool isJump { get; private set; }
    public bool isJumpCanceled { get; private set; }

    public bool isMeleeAttack { get; private set; }
    public bool isMeleeAttackCanceled { get; private set; }

    public bool isParry { get; private set; }
    public bool isParryCanceled { get; private set; }

    public bool isRoll { get; private set; }
    public bool isRollCanceled { get; private set; }

    public bool isInteraction { get; private set; }

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 NormMovementInput { get; private set; }
    public Vector2 MousePosInput { get; private set; }
    public float NormMovementX { get; private set; }
    public float NormMovementY { get; private set; }

    private Vector2 workspace;

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        workspace = RawMovementInput * Vector2.right;
        NormMovementX = workspace.normalized.x;
        workspace = RawMovementInput * Vector2.up;
        NormMovementY = workspace.normalized.y;
        workspace.Set(NormMovementX, NormMovementY);
        NormMovementInput = workspace;
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isJump = true;
                isJumpCanceled = false;
                break;
            case InputActionPhase.Canceled:
                isJumpCanceled = true;
                break;
            default:
                break;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isMeleeAttack = true;
                isMeleeAttackCanceled = false;
                break;
            case InputActionPhase.Canceled:
                isMeleeAttackCanceled = true;
                break;
            default:
                break;
        }
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isParry = true;
                isParryCanceled = false;
                break;
            case InputActionPhase.Canceled:
                isParryCanceled = true;
                break;
            default:
                break;
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isRoll = true;
                isRollCanceled = false;
                break;
            case InputActionPhase.Canceled:
                isRollCanceled = true;
                break;
            default:
                break;
        }

    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isInteraction = true;
                break;
            default:
                break;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                UIEventListener.Instance.OnPauseMenu();
                break;
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        MousePosInput = context.ReadValue<Vector2>();
    }

    public void ResetIsJump() => isJump = false;

    public void ResetIsMeleeAttack() => isMeleeAttack = false;

    public void ResetIsParry() => isParry = false;

    public void ResetIsRoll() => isRoll = false;
    public void ResetIsInteraction() => isInteraction = false;

    public void ResetAll()
    {
        ResetIsJump();
        ResetIsMeleeAttack();
        ResetIsParry();
        ResetIsRoll();
        ResetIsInteraction();
    }

    
}
