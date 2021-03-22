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
    public PlayerStock playerStock;

    [Serializable]
    public struct PlayerStock{
        // enum, count
        public List<ItemData.WeaponRuntimeData> weaponStock;
        public List<ItemData.WearableRuntimeData> wearableStock;
        public List<ItemData.ConsumableRuntimeData> consumableStock;
        public List<ItemData.KeyItemRuntimeData> keyItemStock;
        public PlayerStock(List<ItemData.WeaponRuntimeData> weaponStock, List<ItemData.WearableRuntimeData> wearableStock, 
                List<ItemData.ConsumableRuntimeData> consumableStock, List<ItemData.KeyItemRuntimeData> keyItemStock){
            this.weaponStock = weaponStock;
            this.wearableStock = wearableStock;
            this.consumableStock = consumableStock;
            this.keyItemStock = keyItemStock;
        }

        public void Pick(ItemData.WeaponRuntimeData weapon){
            weaponStock.Add(weapon);
        }

        public void Pick(ItemData.WearableRuntimeData wearable){
            wearableStock.Add(wearable);
        }

        public void Pick(ItemData.ConsumableRuntimeData consumable){
            consumableStock.Add(consumable);
        }

        public void Pick(ItemData.KeyItemRuntimeData keyItem){
            keyItemStock.Add(keyItem);
        }
    };

    [Serializable]
    public struct PlayerEquipment{
        public ItemData.WeaponRuntimeData weapon;
        public ItemData.WearableRuntimeData wearableSlotOne;
        public ItemData.WearableRuntimeData wearableSlotTwo;

        public PlayerEquipment(ItemData.WeaponRuntimeData weapon, ItemData.WearableRuntimeData wearableOne, ItemData.WearableRuntimeData wearableTwo){
            this.weapon = weapon;
            this.wearableSlotOne = wearableOne;
            this.wearableSlotTwo = wearableTwo;
        }        
    };
    
    public void InitPlayerRuntimeData(D_PlayerStateMachine playerData){
        currentHitPoints = playerData.PD_maxHitPoint;
        currentStunPoints = playerData.PD_maxStunPoint;
        currentDecayPoints = 0f;
        currentSceneCode = SceneCode.Gulch_Main;
        lastLittleSunID = -1;
        playerStock = new PlayerStock(new List<ItemData.WeaponRuntimeData>(), new List<ItemData.WearableRuntimeData>(), 
            new List<ItemData.ConsumableRuntimeData>(), new List<ItemData.KeyItemRuntimeData>());
        playerStock.Pick(new ItemData.WeaponRuntimeData(ItemData.Weapon.Iron_Sword, 1));
        playerStock.Pick(new ItemData.WeaponRuntimeData(ItemData.Weapon.Wood_Bow, 1));
        playerStock.Pick(new ItemData.WeaponRuntimeData(ItemData.Weapon.Apprentice_Stick, 0));
        playerStock.Pick(new ItemData.WearableRuntimeData(ItemData.Wearable.Amber_Ring));
        playerStock.Pick(new ItemData.ConsumableRuntimeData(ItemData.Consumable.Holy_Sun_Water, 10));
        playerStock.Pick(new ItemData.KeyItemRuntimeData(ItemData.KeyItem.Dash_Stone));
        
    }

    [Serializable]
    public struct PlayerRuntimeSaveData{
        public float currentHitPoints;
        public float currentStunPoints;
        public float currentDecayPoints;
        public int currentSceneCode;
        public int lastLittleSunID; 
        public string lastPosition;
        public PlayerStock playerStock;

        public PlayerRuntimeSaveData(float currentHitPoints, float currentStunPoints, float currentDecayPoints,
                 SceneCode currentSceneCode, int lastLittleSunID, Vector2 lastPosition, PlayerStock playerStock){
            this.currentHitPoints = currentHitPoints;
            this.currentStunPoints = currentStunPoints;
            this.currentDecayPoints = currentDecayPoints;
            this.currentSceneCode = (int)currentSceneCode;
            this.lastLittleSunID = lastLittleSunID;
            string strLastPos = lastPosition.ToString();
            this.lastPosition = strLastPos.Substring(1, strLastPos.Length-2);
            this.playerStock = playerStock;
            // Debug.Log(this.lastPosition);
        }
    };

    public PlayerRuntimeSaveData GetPlayerRuntimeSaveData(){
        return new PlayerRuntimeSaveData(currentHitPoints, currentStunPoints, currentDecayPoints, currentSceneCode, lastLittleSunID, lastPosition, playerStock);
    }

    public void SetPlayerRuntimeSaveData(PlayerRuntimeSaveData playerRuntimeSaveData){
        currentHitPoints = playerRuntimeSaveData.currentHitPoints;
        currentStunPoints = playerRuntimeSaveData.currentStunPoints;
        currentDecayPoints = playerRuntimeSaveData.currentStunPoints;
        currentSceneCode = (SceneCode)playerRuntimeSaveData.currentSceneCode;
        lastLittleSunID = playerRuntimeSaveData.lastLittleSunID;
        string[] lastPos = playerRuntimeSaveData.lastPosition.Split(',');
        lastPosition = new Vector2(float.Parse(lastPos[0]), float.Parse(lastPos[1]));
        playerStock = playerRuntimeSaveData.playerStock;
        // Debug.Log(playerRuntimeSaveData.lastPosition);
        isLoaded = true;
    }

}
