using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{

    public bool isXReflection;
    public bool isYReflection;

    [Tooltip("Alpha value of reflected sprite, range from 0 to 255")]
    [Range(0, 255)]
    public float startAlpha = 100f;

    [Tooltip("Alpha value of reflected sprite, range from 0 to 255")]
    [Range(0, 255)]
    public float endAlpha = 255f;
    public float alphaDecayRate = 1f;

    public Transform reflectionSource;
    private Transform reflectionCenter;
    private SpriteRenderer sr;
    private SpriteRenderer srSource;

    void Awake(){
        reflectionCenter = transform.Find("Reflection Center");
        sr = transform.Find("Reflection Sprite").GetComponent<SpriteRenderer>();
        srSource = reflectionSource.GetComponent<SpriteRenderer>();

        if(isXReflection){
            sr.flipX = true;
        }
        if(isYReflection){
            sr.flipY = true;
        }
    }

    void Update() {
        if(srSource){
            sr.sprite = srSource.sprite;
            //Debug.Log(sr.sprite);

            Vector2 pos = reflectionSource.position;
            Vector2 offset = Vector2.zero;
            if(isXReflection){
                offset.x = reflectionCenter.position.x - reflectionSource.position.x;
                pos.x = reflectionCenter.position.x + offset.x;
            }
            if(isYReflection){
                offset.y = reflectionCenter.position.y - reflectionSource.position.y;
                pos.y = reflectionCenter.position.y + offset.y;
            }
            sr.transform.position = pos;
            sr.flipX = isXReflection | srSource.flipX;
            sr.flipY = isYReflection | srSource.flipY;

            float alpha = Mathf.Clamp((startAlpha - (Mathf.Abs(offset.y) + Mathf.Abs(offset.x)) * alphaDecayRate) / 255f, endAlpha, startAlpha);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        }
    }
}
