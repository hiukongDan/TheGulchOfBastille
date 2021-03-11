using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Ladder : MonoBehaviour
{
    public int LadderLength = 1;
    void Update(){
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(sr.size.x, LadderLength);
    }

    public LadderPart currentLadderPart;
    private void Awake(){
        currentLadderPart = null;
    }

    #region INTERFACE
    public void OnStartClimbLadder(){
        currentLadderPart?.PlayInfoSignAnim(InfoSignAnimHash.EMPTY);
    }

    public void OnEndClimbLadder(){
        currentLadderPart?.PlayInfoSignAnim(InfoSignAnimHash.INTRO);
    }

    public void OnEnterLadderPart(LadderPart ladderPart){
        currentLadderPart = ladderPart;
    }

    public void OnExitLadderPart() => currentLadderPart = null;

    public LadderPart.Part GetLadderPart() => currentLadderPart!=null?currentLadderPart.ladderPart:LadderPart.Part.BODY;
    #endregion


}
