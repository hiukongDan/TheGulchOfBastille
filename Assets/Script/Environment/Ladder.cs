using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public enum LadderPart
    {
        TOP,
        BODY,
        BUTTOM,
    };

    /* TODO
        fix ladder top and buttom inform player script
        when player at buttom or top, make interaction to climb the ladder
        player exit ladder automaticly when reach top or buttom
    */
    
    public LadderPart ladderPart = LadderPart.BODY;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            switch(ladderPart){
                case LadderPart.BODY:
                    other.gameObject.GetComponent<Player>()?.ladderState.SetLadder(this);
                break;
                case LadderPart.TOP:

                break;
                case LadderPart.BUTTOM:

                break;
                default:
                break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            switch(ladderPart){
                case LadderPart.BODY:

                break;
                case LadderPart.TOP:

                break;
                case LadderPart.BUTTOM:
                
                break;
                default:
                break;
            }
        }
    }
}
