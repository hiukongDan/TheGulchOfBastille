using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRuntimeData
{
    public float currentHitPoints;
    public float currentStunPoints;
    public float currentDecayPoints;
    public float currentUilos;
    public SceneCode currentSceneCode;
    public int lastLittleSunID;
    public Vector2 lastPosition;
    public bool isLoaded = false;
    public PlayerStock playerStock;
    public PlayerSlot playerSlot;

    [Serializable]
    public struct PlayerSlot{
        public int weaponIndex;
        public int wearableOneIndex;
        public int wearableTwoIndex;
        public PlayerSlot(int weaponIndex, int wearableOneIndex, int wearableTwoIndex){
            this.weaponIndex = weaponIndex;
            this.wearableOneIndex = wearableOneIndex;
            this.wearableTwoIndex = wearableTwoIndex;
        }

        public bool IsWeaponInUse(PlayerStock playerStock, int weaponIndex){
            return weaponIndex >= 0 && weaponIndex < playerStock.weaponStock.Count && weaponIndex != this.weaponIndex;
        }

        public bool IsWearableInUse(PlayerStock playerStock, int wearableIndex){
            return wearableIndex >= 0 && wearableIndex < playerStock.wearableStock.Count && 
                (wearableIndex == this.wearableOneIndex || wearableIndex == this.wearableTwoIndex);
        }
    };

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
        currentUilos = 0f;
        currentSceneCode = SceneCode.Gulch_Main;
        lastLittleSunID = -1;
        playerStock = new PlayerStock(new List<ItemData.WeaponRuntimeData>(), new List<ItemData.WearableRuntimeData>(), 
            new List<ItemData.ConsumableRuntimeData>(), new List<ItemData.KeyItemRuntimeData>());

        for(int i = 0; i < (int)ItemData.Weapon.Count; ++i){
            playerStock.Pick(new ItemData.WeaponRuntimeData((ItemData.Weapon)i, i%3));
        }

        for(int i = 0; i < (int)ItemData.Wearable.Count; ++i){
            playerStock.Pick(new ItemData.WearableRuntimeData((ItemData.Wearable)i));
        }

        for(int i = 0; i < (int)ItemData.Consumable.Count; ++i){
            playerStock.Pick(new ItemData.ConsumableRuntimeData((ItemData.Consumable)i, i*10%99 + 1));
        }

        for(int i = 0; i < (int)ItemData.Consumable.Count; ++i){
            playerStock.Pick(new ItemData.ConsumableRuntimeData((ItemData.Consumable)i, i*10%99 + 1));
        }

        for(int i = 0; i < (int)ItemData.Consumable.Count; ++i){
            playerStock.Pick(new ItemData.ConsumableRuntimeData((ItemData.Consumable)i, i*10%99 + 1));
        }

        for(int i = 0; i < (int)ItemData.KeyItem.Count; ++i){
            playerStock.Pick(new ItemData.KeyItemRuntimeData((ItemData.KeyItem)i));
        }

        // use default weapon and none wearables
        playerSlot = new PlayerSlot(0, 0, 1);
    }

    [Serializable]
    public struct PlayerRuntimeSaveData{
        public float currentHitPoints;
        public float currentStunPoints;
        public float currentDecayPoints;
        public float currentUilos;
        public int currentSceneCode;
        public int lastLittleSunID; 
        public string lastPosition;
        public PlayerStock playerStock;
        public PlayerSlot playerSlot;

        public PlayerRuntimeSaveData(float currentHitPoints, float currentStunPoints, float currentDecayPoints, float currentUilos,
                 SceneCode currentSceneCode, int lastLittleSunID, Vector2 lastPosition, PlayerStock playerStock, PlayerSlot playerSlot){
            this.currentHitPoints = currentHitPoints;
            this.currentStunPoints = currentStunPoints;
            this.currentDecayPoints = currentDecayPoints;
            this.currentUilos = currentUilos;
            this.currentSceneCode = (int)currentSceneCode;
            this.lastLittleSunID = lastLittleSunID;
            string strLastPos = lastPosition.ToString();
            this.lastPosition = strLastPos.Substring(1, strLastPos.Length-2);
            this.playerStock = playerStock;
            this.playerSlot = playerSlot;
            // Debug.Log(this.lastPosition);
        }
    };

    public PlayerRuntimeSaveData GetPlayerRuntimeSaveData(){
        isLoaded = false;
        //Debug.Log(currentDecayPoints);
        return new PlayerRuntimeSaveData(currentHitPoints, currentStunPoints, currentDecayPoints, currentUilos, currentSceneCode, lastLittleSunID, lastPosition, playerStock, playerSlot);
    }

    public void SetPlayerRuntimeSaveData(PlayerRuntimeSaveData playerRuntimeSaveData){
        currentHitPoints = playerRuntimeSaveData.currentHitPoints;
        currentStunPoints = playerRuntimeSaveData.currentStunPoints;
        currentDecayPoints = playerRuntimeSaveData.currentStunPoints;
        currentUilos = playerRuntimeSaveData.currentUilos;
        currentSceneCode = (SceneCode)playerRuntimeSaveData.currentSceneCode;
        lastLittleSunID = playerRuntimeSaveData.lastLittleSunID;
        string[] lastPos = playerRuntimeSaveData.lastPosition.Split(',');
        lastPosition = new Vector2(float.Parse(lastPos[0]), float.Parse(lastPos[1]));
        playerStock = playerRuntimeSaveData.playerStock;
        playerSlot = playerRuntimeSaveData.playerSlot;
        // Debug.Log(playerRuntimeSaveData.lastPosition);
        isLoaded = true;
    }

}
