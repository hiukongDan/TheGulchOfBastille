using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    /* TODO
        fix ladder top and buttom inform player script
        when player at buttom or top, make interaction to climb the ladder
        player exit ladder automaticly when reach top or buttom
    */
    
    public LadderPart currentLadderPart;

    private void Awake(){
        currentLadderPart = null;
    }

    #region INTERFACE
    public void OnStartClimbLadder(){
        currentLadderPart.infoSignAnim.Play(InfoSignAnimHash.EMPTY);
    }

    public void OnEndClimbLadder(){

    }

    public void OnEnterLadderPart(LadderPart ladderPart){
        currentLadderPart = ladderPart;
    }

    public void OnExitLadderPart() => currentLadderPart = null;

    public LadderPart.Part GetLadderPart() => currentLadderPart!=null?currentLadderPart.ladderPart:LadderPart.Part.BODY;
    #endregion


}
