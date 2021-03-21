using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class UIIconLoader : MonoBehaviour
{
    public static string WEAPON_PATH = "Icon/Weapon";
    public static string WEARABLE_PATH = "Icon/Wearable";
    public static string CONSUMBALE_PATH = "Icon/Consumable";
    public static string KEYITEM_PATH = "Icon/KeyItem";
    public static Dictionary<int, Sprite> WeaponIcons = new Dictionary<int, Sprite>();
    public static Dictionary<int, Sprite> WearableIcons = new Dictionary<int, Sprite>();
    public static Dictionary<int, Sprite> ConsumableIcons = new Dictionary<int, Sprite>();
    public static Dictionary<int, Sprite> KeyItemIcons = new Dictionary<int, Sprite>();

    void Awake(){
        // Sprite iron_sword = Resources.Load<Sprite>(WeaponPath + "/" + ItemData.Weapon.Iron_Sword.ToString());
        // Debug.Log(iron_sword);
        LoadIconResources();
    }

    void LoadIconResources(){
        WeaponIcons.Clear();
        WearableIcons.Clear();
        ConsumableIcons.Clear();
        KeyItemIcons.Clear();
        
        for(int i = 0; i < (int)ItemData.Weapon.Count; ++i){
            string path = WEAPON_PATH + "/" + ((ItemData.Weapon)i).ToString();
            WeaponIcons.Add(i, Resources.Load<Sprite>(path));
        }

        for(int i = 0; i < (int)ItemData.Wearable.Count; ++i){
            string path = WEARABLE_PATH + "/" + ((ItemData.Wearable)i).ToString();
            WearableIcons.Add(i, Resources.Load<Sprite>(path));
        }

        for(int i = 0; i < (int)ItemData.Consumable.Count; ++i){
            string path = CONSUMBALE_PATH + "/" + ((ItemData.Consumable)i).ToString();
            ConsumableIcons.Add(i, Resources.Load<Sprite>(path));
        }

        for(int i = 0; i < (int)ItemData.KeyItem.Count; ++i){
            string path = KEYITEM_PATH + "/" + ((ItemData.KeyItem)i).ToString();
            KeyItemIcons.Add(i, Resources.Load<Sprite>(path));
        }

        Debug.Log("Weapon: " + WeaponIcons.Count + " loaded");
        Debug.Log("Wearable: " + WearableIcons.Count + " loaded");
        Debug.Log("Consumable: " + ConsumableIcons.Count + " loaded");
        Debug.Log("KeyItem: " + KeyItemIcons.Count + " loaded");
    }


}
