using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int hashCode = GetHashCode();

        var dict = getDict();
        if (dict.ContainsKey(hashCode))
        {
            dict[hashCode] = isAlive;
        }
        else
        {
            dict.Add(hashCode, isAlive);
        }

        // Debug.Log(dict[hashCode]);
    }

    public bool IsAlive()
    {
        int hashCode = GetHashCode();
        bool ret = defaultAlive;
        var dict = getDict();
        if (dict.ContainsKey(hashCode))
        {
            //Debug.Log("contains key" + hashCode);
            ret = dict[hashCode];
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

    public static void SetEnemyData(Dictionary<int, bool> enemyAliveRevivable, Dictionary<int, bool> enemyAliveUnrevivable){
        if(EnemySaveData.EnemyAliveRevivable == null){
            EnemySaveData.EnemyAliveRevivable = new Dictionary<int, bool>();
        }
        if(EnemySaveData.EnemyAliveUnrevivable == null){
            EnemySaveData.EnemyAliveUnrevivable = new Dictionary<int, bool>();
        }
        Initialize();

        foreach(KeyValuePair<int, bool> item in enemyAliveRevivable){
            EnemySaveData.EnemyAliveRevivable.Add(item.Key, item.Value);
        }
        foreach(KeyValuePair<int, bool> item in enemyAliveUnrevivable){
            EnemySaveData.EnemyAliveUnrevivable.Add(item.Key, item.Value);
        }
    }

}
