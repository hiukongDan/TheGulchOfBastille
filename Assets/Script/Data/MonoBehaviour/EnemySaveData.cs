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

    public void Initialize()
    {
        EnemyAliveRevivable.Clear();
        EnemyAliveUnrevivable.Clear();
    }

}
