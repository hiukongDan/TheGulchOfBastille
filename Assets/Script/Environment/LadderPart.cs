using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPart : MonoBehaviour
{
    public enum Part
    {
        TOP,
        BODY,
        BUTTOM,
    };

    public Part ladderPart;

    private Ladder ladder; // the ladder this part belongs to
    public Animator infoSignAnim;

    private string str_IsEmpty = "IsEmpty";


    private void Awake(){
        ladder = GetComponentInParent<Ladder>();
        infoSignAnim = GetComponentInChildren<Animator>();
    }

    private void Start(){
        infoSignAnim.SetBool(str_IsEmpty, true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        ladder.OnEnterLadderPart(this);
        Player player = other.gameObject.GetComponent<Player>();
        if(player && player.stateMachine.currentState != player.ladderState && infoSignAnim.GetBool(str_IsEmpty)){
            player.ladderState.SetLadder(ladder);
            infoSignAnim.Play(InfoSignAnimHash.INTRO);
            infoSignAnim.SetBool(str_IsEmpty, false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        ladder.OnExitLadderPart();
        Player player = other.gameObject.GetComponent<Player>();
        if(player && player.stateMachine.currentState != player.ladderState && !infoSignAnim.GetBool(str_IsEmpty)){
            player.ladderState.UnSetLadder();
            infoSignAnim.Play(InfoSignAnimHash.OUTRO);
            infoSignAnim.SetBool(str_IsEmpty, true);
        }
    }
    
}
