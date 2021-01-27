using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSunData : MonoBehaviour
{
    private static LittleSunData instance;
    public static LittleSunData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LittleSunData();
            }
            return instance;
        }
    }

    public enum LittleSunID
    {
        TheGulch_0,
        Count
    }

    public Dictionary<int, bool> littleSunLit;

    public LittleSunData()
    {
        littleSunLit = new Dictionary<int, bool>();
        for(int i = 0; i < (int)LittleSunID.Count; i++)
        {
            littleSunLit[i] = false;
        }
    }

    public bool IsLit(LittleSunID littleSunID)
    {
        return littleSunLit.ContainsKey((int)littleSunID) && littleSunLit[(int)littleSunID];
    }
    
}
