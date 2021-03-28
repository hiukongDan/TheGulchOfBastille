using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot: MonoBehaviour{
    // key: loot hash code, value: isValid
    public static Dictionary<int, bool> lootDict = new Dictionary<int, bool>();
    protected Transform alive;
    protected Animator infoAnim;
    protected Animator indicatorAnim;
    
    void Awake(){
        alive = transform.Find("Alive");
        infoAnim = transform.Find("Alive/InfoSign").GetComponentInChildren<Animator>();
        indicatorAnim = transform.Find("Alive/Indicator").GetComponent<Animator>();
    }

    void OnEnable() {
        if(alive == null){
            alive = transform.Find("Alive");
        }

        if(isValid()){
            alive.gameObject.SetActive(true);
        }
        else{
            alive.gameObject.SetActive(false);
        }
    }

    void OnDisable() {
        alive.gameObject.SetActive(false);
    }

    protected virtual void OnEnterLootArea(Player player){
        player.SetLootHandler(this);
        infoAnim.Play(InfoSignAnimHash.INTRO);
    }

    protected virtual void OnExitLootArea(Player player){
        player.SetLootHandler(null);
        infoAnim.Play(InfoSignAnimHash.OUTRO);
    }

    public virtual void OnPickUpLoot(Player player){
        if(Loot.lootDict.ContainsKey(GetHashCode())){
            Loot.lootDict[GetHashCode()] = false;
        }
        else{
            Loot.lootDict.Add(GetHashCode(), false);
        }
        alive.gameObject.SetActive(false);
        player.SetLootHandler(null);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(!isValid()){
            return;
        }

        if(other.gameObject.tag == "Player"){
            OnEnterLootArea(other.gameObject.GetComponent<Player>());
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if(!isValid()){
            return;
        }

        if(other.gameObject.tag == "Player"){
            OnExitLootArea(other.gameObject.GetComponent<Player>());
        }
    }

    protected bool isValid(){
        if(Loot.lootDict.ContainsKey(GetHashCode())){
            return Loot.lootDict[GetHashCode()];
        }   
        else{
            Loot.lootDict.Add(GetHashCode(), true);
        }
        return true;
    }

    public static void Initialize(){
        lootDict.Clear();
    }

    public static void OnPickUpLoot(Player player, ItemData.Weapon weapon, int level){
        player.playerRuntimeData.playerStock.Pick(new ItemData.WeaponRuntimeData(weapon, level));
        string info = "Pick up " + string.Join(" ", weapon.ToString().Split('_')) + (level>0?" level "+level:"");
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData(info));
    }
    public static void OnPickUpLoot(Player player, ItemData.Wearable wearable){
        player.playerRuntimeData.playerStock.Pick(new ItemData.WearableRuntimeData(wearable));
        string info = "Pick up " + string.Join(" ", wearable.ToString().Split('_'));
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData(info));
    }
    public static void OnPickUpLoot(Player player, ItemData.Consumable consumable, int amount){
        player.playerRuntimeData.playerStock.Pick(new ItemData.ConsumableRuntimeData(consumable, amount));
        string info = "Pick up " + string.Join(" ", consumable.ToString().Split('_') + " Amount " + amount);
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData(info));
    }

    public static void OnPickUpLoot(Player player, ItemData.KeyItem keyItem){
        player.playerRuntimeData.playerStock.Pick(new ItemData.KeyItemRuntimeData(keyItem));
        string info = "Pick up " + string.Join(" ", keyItem.ToString().Split('_'));
        UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData(info));
    }



}