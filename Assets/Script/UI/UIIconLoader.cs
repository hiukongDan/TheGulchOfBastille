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
    public static Sprite EmptySprite;
    public static Dictionary<int, Sprite> WeaponIcons = new Dictionary<int, Sprite>();
    public static Dictionary<int, Sprite> WearableIcons = new Dictionary<int, Sprite>();
    public static Dictionary<int, Sprite> ConsumableIcons = new Dictionary<int, Sprite>();
    public static Dictionary<int, Sprite> KeyItemIcons = new Dictionary<int, Sprite>();

    void Awake(){
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

        EmptySprite = Resources.Load<Sprite>("Icon/Empty_Icon");
    }


}
