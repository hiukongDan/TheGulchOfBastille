using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UilosGroup : MonoBehaviour
{
    [Tooltip("Determine which value to use")]
    public bool isConstant = true;
    public int uilosCount;
    public Vector2 uilosCountRange;
    public GameObject uilosPref;

    public void OnGenerateUilos(Vector3 position){
        int count = 0;
        if(isConstant){
            count = uilosCount;
        }
        else{
            count = (int)Random.Range(uilosCountRange.x, uilosCountRange.y);
        }
        for(int i = 0; i < count; ++i){
            GameObject.Instantiate(uilosPref, position, transform.rotation, transform);
        }
    }
}
