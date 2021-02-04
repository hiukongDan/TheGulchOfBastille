using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newLittleSunData", menuName ="Data/Other/LittleSunData")]
public class LittleSunData:ScriptableObject
{
    public static Dictionary<int, bool> LittleSuns = new Dictionary<int, bool>();

    public int LittleSunID;
    public bool isActive = false;
    public LittleSunData()
    {
        if (LittleSuns.ContainsKey(LittleSunID))
        {
            isActive = LittleSuns[LittleSunID];
        }
        else         // else use default value
        {
            LittleSuns.Add(LittleSunID, isActive);
        }
    }

    public bool IsActive() => isActive;

    public void OnLightLittleSun()
    {
        isActive = true;
        LittleSuns[LittleSunID] = true;
    }
    
}
