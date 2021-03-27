using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSunMenu : MonoBehaviour
{
    public Transform alive{get; private set;}
    public List<Transform> entries{get; private set;}
    public Transform selection{get; private set;}

    public int currentEntry{get; private set;}
    public int currentSelection{get; private set;}

    private GameManager gameManager;

    // local cache
    private List<LittleSunData> litLittleSun;

    void Awake(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(entries == null){
            entries = new List<Transform>();
        }
        else{
            entries.Clear();
        }

        Transform entriesTrans = transform.Find("Alive/Entries");
        for(int i = 0 ; i < entriesTrans.childCount; ++i){
            entries.Add(entriesTrans.GetChild(i));
        }
        selection = transform.Find("Alive/Selection");
        alive = transform.Find("Alive");

        // Debug.Log("alive" + entries.Count + " " + selection.name + " " + alive.name);
    }

    public void Activate(){
        alive.gameObject.SetActive(true);
    }

    public void Deactivate(){
        alive.gameObject.SetActive(false);
    }

    public void ResetMenu(){
        currentEntry = currentSelection = 0;
        litLittleSun = gameManager.GetLitLittleSun();
        refreshMenu();
    }

    public void SelectPrevious(){
        if(currentSelection > 0){
            currentSelection--;
            if(currentEntry > 0){
                currentEntry--;
            }
        }
        refreshMenu();
    }

    public void SelectNext(){
        if(currentSelection + 1 < litLittleSun.Count){
            currentSelection++;
            if(currentEntry + 1 < entries.Count){
                currentEntry++;
            }   
        }
        refreshMenu();
    }

    public LittleSunData GetCurrentSelectedLittleSun(){
        return litLittleSun[currentSelection];
    }

    private void refreshMenu(){
        int totalPlaces = litLittleSun.Count;
        int i = 0;
        int offset = currentSelection - currentEntry;
        for(; i < entries.Count && i + offset < totalPlaces; ++i){
            entries[i].GetComponent<PFontSprite>().SetText(ParseSceneCode(litLittleSun[i+offset].sceneCode));
        }

        if(i < entries.Count){
            for(; i < entries.Count; ++i){
                entries[i].GetComponent<PFontSprite>().SetText("undiscovered");
            }
        }

        selection.position = new Vector2(selection.position.x, entries[currentEntry].position.y);
    }

    public string ParseSceneCode(SceneCode sceneCode){
        string res = "";
        string[] words = sceneCode.ToString().Split('_');
        for(int i = 0; i < words.Length && i < 2; ++i){
            res += words[i] + " ";
        }
        return res.Trim();
    }
    
}
