using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgRoll : MonoBehaviour
{
    /* Roll Backgorund horizontally */
    public float Speed = 1f;
    // public float Direction = 1f; // direction > 0 for left, direction < 0 for right

    List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    void OnEnable()
    {
        // Get Child
        if(sprites.Count == 0){
            foreach(var child in GetComponentsInChildren<SpriteRenderer>()){
                sprites.Add(child);
            }
        }

        // Repositioning
        float currentPosition = 0f;
        foreach(SpriteRenderer sr in sprites){
            sr.transform.localPosition = new Vector2(currentPosition, sr.transform.localPosition.y);
            currentPosition += sr.sprite.rect.width/sr.sprite.pixelsPerUnit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(sprites.Count > 0){
            foreach(SpriteRenderer sr in sprites){
                if(sr.transform.localPosition.x < -(sr.sprite.rect.xMax-sr.sprite.rect.x)/sr.sprite.pixelsPerUnit){
                    sr.transform.localPosition = new Vector2((sprites.Count-1) * (sr.sprite.rect.width/sr.sprite.pixelsPerUnit), sr.transform.localPosition.y);
                }
                sr.transform.localPosition = new Vector2(sr.transform.localPosition.x - Speed * Time.deltaTime, sr.transform.localPosition.y);
            }
        }
    }

    
}
