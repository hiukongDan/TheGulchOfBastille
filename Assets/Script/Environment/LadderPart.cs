using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
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

    public static string str_IsEmpty = "IsEmpty";


    private void Awake(){
        ladder = GetComponentInParent<Ladder>();
        infoSignAnim = GetComponentInChildren<Animator>();
    }

    private void Start(){
        infoSignAnim.SetBool(str_IsEmpty, true);
    }


    private void Update(){
        SpriteRenderer sr = ladder.GetComponent<SpriteRenderer>();
        BoxCollider2D bc2d = GetComponent<BoxCollider2D>();    
        float height = sr.size.y;
        switch(ladderPart){
            case Part.TOP:
                transform.position = new Vector2(transform.position.x, 
                ladder.transform.position.y+height/2+bc2d.size.y/2);
            break;
            case Part.BUTTOM:
                transform.position = new Vector2(transform.position.x, 
                ladder.transform.position.y-height/2+bc2d.size.y/2);
            break;
            default:
            break;
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        ladder.OnEnterLadderPart(this);
        Player player = other.gameObject.GetComponent<Player>();
        if(player && player.stateMachine.currentState != player.ladderState && infoSignAnim.GetBool(str_IsEmpty)){
            player.ladderState.SetLadder(ladder);
            PlayInfoSignAnim(InfoSignAnimHash.INTRO);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        ladder.OnExitLadderPart();
        Player player = other.gameObject.GetComponent<Player>();
        if(player && player.stateMachine.currentState != player.ladderState && !infoSignAnim.GetBool(str_IsEmpty)){
            player.ladderState.UnSetLadder();
            PlayInfoSignAnim(InfoSignAnimHash.OUTRO);
        }
    }

    public void PlayInfoSignAnim(int infoSignAnimHash){
        infoSignAnim.Play(infoSignAnimHash);
        if(infoSignAnimHash == InfoSignAnimHash.EMPTY || infoSignAnimHash == InfoSignAnimHash.OUTRO){
            infoSignAnim.SetBool(str_IsEmpty, true);
        }
        else if(infoSignAnimHash == InfoSignAnimHash.INTRO || infoSignAnimHash == InfoSignAnimHash.IDLE){
            infoSignAnim.SetBool(str_IsEmpty, false);
        }
    }
    
}
