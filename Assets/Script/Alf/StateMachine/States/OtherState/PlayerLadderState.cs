using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderState : PlayerState
{
    private float gravityOld;
    private Ladder ladder = null;

    private bool isJumpOff = false;
    private bool isClimbing = false;

    private bool isReachLadderEnd = false;

    public PlayerLadderState(PlayerStateMachine stateMachine, Player player, int defaultAnimCode, D_PlayerStateMachine data):
        base(stateMachine, player, defaultAnimCode, data)
    {

    }

    public override void Enter()
    {
        player.SwitchAC(Player.AC_TYPE.ROOT_MOTION);

        player.InputHandler.ResetIsInteraction();

        player.SetVelocity(Vector2.zero);

        ladder?.OnStartClimbLadder();

        workspace.Set(ladder.transform.position.x, player.transform.position.y);
        player.SetPosition(workspace);

        gravityOld = player.Rb.gravityScale;
        player.Rb.gravityScale = 0f;

        isJumpOff = false;
        isClimbing = false;

        if(ladder.GetLadderPart() == LadderPart.Part.TOP){
            player.Anim.Play(AlfAnimationHash.LADDER_TOP_START_0);
        }
        else if(ladder.GetLadderPart() == LadderPart.Part.BUTTOM){
            player.Anim.Play(AlfAnimationHash.LADDER_BUTTOM_START_0);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SwitchAC(Player.AC_TYPE.NORMAL);

        player.Rb.gravityScale = gravityOld;

        isJumpOff = false;
        isClimbing = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isClimbing){
            if (isJumpOff){
                workspace.Set(0, -data.LS_jumpOffSpeed);
                player.SetVelocity(workspace);
            }
            else if(isJump){
                isJumpOff = true;
            }
            else if(isReachLadderEnd){
                OnReachOneEnd();
            }
            else{
                player.Anim.SetFloat("ladderClimbDirection", normMovementInput.y);
            }
        }
    }

    protected override void DoCheck()
    {
        isReachLadderEnd = player.CheckLadderEnd();
    }

#region INTERFACE
    public void SetLadder(Ladder ladder) => this.ladder = ladder;
    public void UnSetLadder() => this.ladder = null;
    public bool HasValidLadder() => this.ladder != null;
    private void OnReachOneEnd(){
        isClimbing = false;
        // Play Reach One End Anim
        if(ladder.GetLadderPart() == LadderPart.Part.TOP){
            player.Anim.Play(AlfAnimationHash.LADDER_TOP_FINISH_0);
        }
        else if(ladder.GetLadderPart() == LadderPart.Part.BUTTOM){
            player.Anim.Play(AlfAnimationHash.LADDER_BUTTOM_FINISH_0);
        }
    }
    public void CompleteStartClimbLadder(){
        isClimbing = true;
        Debug.Log("start");
    } 
    public void CompleteClimbLadder(){
        stateMachine.SwitchState(player.idleState);
        Debug.Log("called complete");
    }
#endregion

}
