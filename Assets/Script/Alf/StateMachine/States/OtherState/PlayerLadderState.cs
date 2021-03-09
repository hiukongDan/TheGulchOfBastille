using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderState : PlayerState
{
    private float gravityOld;
    private Ladder ladder = null;
    public PlayerLadderState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data):
        base(stateMachine, player, defaultAnimCode, data)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);

        //workspace.Set(ladder.transform.position.x, player.transform.position.y);
        //player.SetPosition(workspace);

        gravityOld = player.Rb.gravityScale;
        player.Rb.gravityScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        player.Rb.gravityScale = gravityOld;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(shouldFlip){
            player.Flip();
        }

        if(isJump){
            // move to InAir state
            if(normMovementInput.y > 0f){
                stateMachine.SwitchState(player.jumpState);
            }
            else{
                stateMachine.SwitchState(player.inAirState);
            }
        }
        else if(!isLadder){
            workspace.Set((normMovementInput.x == 0? 1 : normMovementInput.x) * data.LS_leaveHorizontalImpulse, data.LS_leaveVerticalImpulse);
            player.SetVelocity(workspace);
            player.Rb.AddForce(workspace, ForceMode2D.Impulse);
            stateMachine.SwitchState(player.inAirState);
        }
        else {
            workspace.Set(0, data.LS_climbSpeed * normMovementInput.y);
            player.SetVelocity(workspace);
        }
    }

    

#region INTERFACE
    public void SetLadder(Ladder ladder) => this.ladder = ladder;
    public void UnSetLadder() => this.ladder = null;
    public bool HasValidLadder() => this.ladder != null;
#endregion

}
