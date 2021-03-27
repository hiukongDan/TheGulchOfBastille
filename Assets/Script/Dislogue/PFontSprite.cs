using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class PFontSprite : MonoBehaviour
{
    public PFontInfo pFontInfo;
    public string text;
    public float pixelsPerUnit = 32f;
    private SpriteRenderer spriteRenderer;
    
    private PFontLoader pFontLoader;
    void Awake(){
        
    }

    void OnEnable() {
        SetText(text);
    }
    
    void Start(){
        
    }

    public void SetText(string text){
        pFontLoader = new PFontLoader(pFontInfo);
        this.text = parseString(text);
        UpdateView();
    }

    public void UpdateView(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(this.text == ""){
            spriteRenderer.sprite = null;
        }

        Sprite sprite = generateSprite(this.text);
        spriteRenderer.sprite = sprite;
    }

    private ref Texture2D InitTexture2D(ref Texture2D texture){
        Color[] cols = texture.GetPixels();
        Color transparent = new Color(0,0,0,0);
        for(int i = 0; i < cols.Length; ++i){
            cols[i] = transparent;
        }
        texture.SetPixels(cols);
        texture.Apply();
        return ref texture;
    }

    private Sprite generateSprite(string text){
        Texture2D texture = new Texture2D((int)pFontLoader.GetLengthInPixel(text), (int)pFontLoader.charHeightInPixel);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;

        texture = InitTexture2D(ref texture);

        Color[] cols = texture.GetPixels();

        int offset = 0;
        foreach(char c in text.ToCharArray()){
            PFontLoader.CharInfo chInfo = pFontLoader.chars[c];
            
            Rect rect = chInfo.sprite.rect;
            Color[] chCols = chInfo.sprite.texture.GetPixels((int)rect.xMin, (int)rect.yMin, (int)rect.width, (int)rect.height);
            for(int i = 0; i < chCols.Length; ++i){
                int row = i / (int)chInfo.sprite.rect.width;
                int col = i % (int)chInfo.sprite.rect.width;
                cols[row*texture.width + offset + col] = chCols[i];
            }

            offset += (int)chInfo.sprite.rect.width;
        }

        texture.SetPixels(cols);
        texture.Apply();


        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
    }

    private string parseString(string text){
        string res = "";
        text = text.ToUpper();
        foreach(char c in text.ToCharArray()){
            if(pFontLoader.chars.ContainsKey(c)){
                res += c;
            }
        }
        return res;
    }

}
