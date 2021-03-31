using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class GulchGUID : MonoBehaviour
{
    public static List<string> ids;
    public static List<GameObject> gos;

    [SerializeField]
    private string id;

    public string ID{
        get{
            if(id == ""){
                id = System.Guid.NewGuid().ToString();
                PrefabUtility.RecordPrefabInstancePropertyModifications(this);
            }
            return id;
        }
    }
    
    void Awake(){
        if(id == ""){
            id = System.Guid.NewGuid().ToString();
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);
        }
    }
}