using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[CreateAssetMenu(fileName ="newEnemySaveData", menuName ="Data/EnemyData/SaveData")]
public class EnemySaveData : MonoBehaviour
{
    public static Dictionary<int, bool> EnemyAliveRevivable = new Dictionary<int, bool>();
    public static Dictionary<int, bool> EnemyAliveUnrevivable = new Dictionary<int, bool>();

    public bool isRevivable = true;
    public bool defaultAlive = true;

    private Dictionary<int, bool> getDict()
    {
        Dictionary<int, bool> dict;
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
        int id = GetInstanceID();

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
        int id = GetInstanceID();
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
        List<int> keys = new List<int>(EnemyAliveRevivable.Keys);
        foreach(int key in keys){
            EnemyAliveRevivable[key] = true;
        }
    }

    [Serializable]
    public struct EnemyRuntimeSaveData{
        public Dictionary<int, bool> enemyAliveRevivable;
        public Dictionary<int, bool> enemyAliveUnrevivable;

        public EnemyRuntimeSaveData(Dictionary<int, bool> enemyAliveRevivable, Dictionary<int, bool> enemyAliveUnrevivable){
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
