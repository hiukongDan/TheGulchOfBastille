using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<Transform> tabs;

    private GameManager gameManager;

    void Awake(){
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnEnable() {
        if(tabs == null){
            tabs = new List<Transform>();
        }
        else{
            tabs.Clear();
        }

        for(int i = 0; i < transform.childCount; ++i){
            
        }
        
    }
}
