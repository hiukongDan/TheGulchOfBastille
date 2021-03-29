using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="newLittleSunData", menuName ="Data/Other/LittleSunData")]
public class LittleSunData : MonoBehaviour
{
    public static Dictionary<int, bool> LittleSuns = new Dictionary<int, bool>();
    public int LittleSunID;
    public SceneCode sceneCode;
    public Transform TeleportPoint;
    void Awake(){
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

    public static void Initialize(){
        if(LittleSunData.LittleSuns == null){
            LittleSuns = new Dictionary<int, bool>();
        }
        else{
            LittleSuns.Clear();
        }
    }

    public static void SetLittleSunData(Dictionary<int, bool> littleSuns){
        if(LittleSunData.LittleSuns == null){
            LittleSuns = new Dictionary<int, bool>();
        }
        else{
            LittleSuns.Clear();
        }
        foreach(KeyValuePair<int, bool> item in littleSuns){
            LittleSuns.Add(item.Key, item.Value);
        }
    }
    
}
