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
    private BoxCollider2D boxCollider2D;
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

        player.Bc.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.SwitchAC(Player.AC_TYPE.NORMAL);

        player.Rb.gravityScale = gravityOld;

        isJumpOff = false;
        isClimbing = false;

        player.InputHandler.ResetAll();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isReachLadderEnd = player.CheckLadderEnd();

        if(isClimbing){
            if(isReachLadderEnd){
                OnReachOneEnd();
                player.Anim.applyRootMotion = true;
            }
            else if (isJumpOff){
                if(isJump){
                    isJumpOff = false;
                    player.SetVelocityY(0);
                    player.Anim.applyRootMotion = true;
                    player.Anim.Play(AlfAnimationHash.LADDER_CLIMB_0);

                    player.InputHandler.ResetIsJump();
                    isJump = false;
                }
                else{
                    workspace.Set(0, -data.LS_jumpOffSpeed);
                    player.SetVelocity(workspace);
                }
            }
            else if(isJump){
                isJumpOff = true;
                player.Anim.applyRootMotion = false;
                player.Anim.Play(AlfAnimationHash.LADDER_SLIDE_0);

                player.InputHandler.ResetIsJump();
                isJump = false;
            }
            else{
                player.Anim.SetFloat("ladderClimbDirection", normMovementInput.y);
            }
        }
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
        player.Bc.enabled = false;
    }
    public void CompleteStartClimbLadder(){
        isClimbing = true;
        player.Bc.enabled = true;
    } 
    public void CompleteClimbLadder(){
        player.Bc.enabled = true;
        stateMachine.SwitchState(player.idleState);
        ladder?.OnEndClimbLadder();
    }
#endregion

}
