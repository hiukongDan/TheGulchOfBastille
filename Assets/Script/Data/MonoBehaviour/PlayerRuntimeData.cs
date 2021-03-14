using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRuntimeData
{
    public float currentHitPoints;
    public float currentStunPoints;
    public float currentDecayPoints;
    public SceneCode currentSceneCode;
    public int lastLittleSunID;
    public Vector2 lastPosition;
    public bool isLoaded = false;

    public void InitPlayerRuntimeData(D_PlayerStateMachine playerData){
        currentHitPoints = playerData.PD_maxHitPoint;
        currentStunPoints = playerData.PD_maxStunPoint;
        currentDecayPoints = 0f;
        currentSceneCode = SceneCode.Gulch_Main;
        lastLittleSunID = -1;
    }

    [Serializable]
    public struct PlayerRuntimeSaveData{
        public float currentHitPoints;
        public float currentStunPoints;
        public float currentDecayPoints;
        public int currentSceneCode;
        public int lastLittleSunID; 
        public string lastPosition;

        public PlayerRuntimeSaveData(float currentHitPoints, float currentStunPoints, float currentDecayPoints,
                 SceneCode currentSceneCode, int lastLittleSunID, Vector2 lastPosition){
            this.currentHitPoints = currentHitPoints;
            this.currentStunPoints = currentStunPoints;
            this.currentDecayPoints = currentDecayPoints;
            this.currentSceneCode = (int)currentSceneCode;
            this.lastLittleSunID = lastLittleSunID;
            string strLastPos = lastPosition.ToString();
            this.lastPosition = strLastPos.Substring(1, strLastPos.Length-2);
            // Debug.Log(this.lastPosition);
        }
    };

    public PlayerRuntimeSaveData GetPlayerRuntimeSaveData(){
        return new PlayerRuntimeSaveData(currentHitPoints, currentStunPoints, currentDecayPoints, currentSceneCode, lastLittleSunID, lastPosition);
    }

    public void SetPlayerRuntimeSaveData(PlayerRuntimeSaveData playerRuntimeSaveData){
        currentHitPoints = playerRuntimeSaveData.currentHitPoints;
        currentStunPoints = playerRuntimeSaveData.currentStunPoints;
        currentDecayPoints = playerRuntimeSaveData.currentStunPoints;
        currentSceneCode = (SceneCode)playerRuntimeSaveData.currentSceneCode;
        lastLittleSunID = playerRuntimeSaveData.lastLittleSunID;
        string[] lastPos = playerRuntimeSaveData.lastPosition.Split(',');
        lastPosition = new Vector2(float.Parse(lastPos[0]), float.Parse(lastPos[1]));
        // Debug.Log(playerRuntimeSaveData.lastPosition);
        isLoaded = true;
    }

}
