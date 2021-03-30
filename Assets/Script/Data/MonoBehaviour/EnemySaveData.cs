using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[CreateAssetMenu(fileName ="newEnemySaveData", menuName ="Data/EnemyData/SaveData")]
public class EnemySaveData : MonoBehaviour
{
    public static Dictionary<string, bool> EnemyAliveRevivable = new Dictionary<string, bool>();
    public static Dictionary<string, bool> EnemyAliveUnrevivable = new Dictionary<string, bool>();

    public bool isRevivable = true;
    public bool defaultAlive = true;

    private Dictionary<string, bool> getDict()
    {
        Dictionary<string, bool> dict;
        if (isRevivable)
        {
            dict = EnemyAliveRevivable;
        }
        else
        {
            dict = EnemyAliveUnrevivable;
        }
        return dict;
    }

    public void Save(bool isAlive)
    {
        string id = GetComponent<GulchGUID>().ID;

        var dict = getDict();
        if (dict.ContainsKey(id))
        {
            dict[id] = isAlive;
        }
        else
        {
            dict.Add(id, isAlive);
        }

        // Debug.Log(dict[hashCode]);
    }

    public bool IsAlive()
    {
        string id = GetComponent<GulchGUID>().ID;
        bool ret = defaultAlive;
        var dict = getDict();
        if (dict.ContainsKey(id))
        {
            //Debug.Log("contains key" + hashCode);
            ret = dict[id];
        }
        return ret;
    }

    public static void Initialize()
    {
        EnemyAliveRevivable.Clear();
        EnemyAliveUnrevivable.Clear();
    }

    public static void ResetRevivableEnemy(){
        List<string> keys = new List<string>(EnemyAliveRevivable.Keys);
        foreach(string key in keys){
            EnemyAliveRevivable[key] = true;
        }
    }

    [Serializable]
    public struct EnemyRuntimeSaveData{
        public Dictionary<string, bool> enemyAliveRevivable;
        public Dictionary<string, bool> enemyAliveUnrevivable;

        public EnemyRuntimeSaveData(Dictionary<string, bool> enemyAliveRevivable, Dictionary<string, bool> enemyAliveUnrevivable){
            this.enemyAliveRevivable = enemyAliveRevivable;
            this.enemyAliveUnrevivable = enemyAliveUnrevivable;
        }
    };

    public static EnemyRuntimeSaveData GetEnemyRuntimeSaveData(){
        return new EnemyRuntimeSaveData(EnemyAliveRevivable, EnemyAliveUnrevivable);
    }

    public static void SetEnemyRuntimeSaveData(EnemyRuntimeSaveData enemyRuntimeSaveData){
        EnemyAliveRevivable = enemyRuntimeSaveData.enemyAliveRevivable;
        EnemyAliveUnrevivable = enemyRuntimeSaveData.enemyAliveUnrevivable;
    }

}
