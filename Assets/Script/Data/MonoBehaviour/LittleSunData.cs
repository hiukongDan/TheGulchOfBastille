using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="newLittleSunData", menuName ="Data/Other/LittleSunData")]
public class LittleSunData:MonoBehaviour
{
    public static Dictionary<int, bool> LittleSuns = new Dictionary<int, bool>();

    public int LittleSunID;
    public SceneCode sceneCode;

    public Transform TeleportPoint{get; private set;}
    void Awake(){
        TeleportPoint = transform.Find("TeleportPoint");
    }

    public LittleSunData()
    {
        if(!LittleSuns.ContainsKey(LittleSunID))
            LittleSuns.Add(LittleSunID, false);
    }

    public bool IsActive() => LittleSuns.ContainsKey(LittleSunID) ? LittleSuns[LittleSunID] : false;

    public void OnLightLittleSun()
    {
        if(!LittleSuns.ContainsKey(LittleSunID))
        {
            LittleSuns.Add(LittleSunID, true);
        }
        else
        {
            LittleSuns[LittleSunID] = true;
        }
    }
    
}
