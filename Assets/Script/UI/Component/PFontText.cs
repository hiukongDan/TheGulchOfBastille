using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PFontText : MonoBehaviour
{
    public PFontInfo pFontInfo;
    public float pixelPerUnit = 32f;
    public string text;

    [Range(0, 10)]
    public int marginPixel = 1;
    private PFontLoader pFontLoader;
    private Image displayImage;

    public void SetText(string text){
        this.text = text;
        OnTextChange();
    }

    public int GetTotalPixelWidth(string str){
        int res = 0;
        foreach(char c in str){
            res += (int)pFontLoader.chars[c].width;
        }
        return res;
    }

    // void OnValidate(){
    //     OnTextChange();
    // }

    public void OnTextChange(){
        if(text.Length == 0)
            return;

        pFontLoader = new PFontLoader(pFontInfo);
        displayImage = GetComponent<Image>();

        text = text.ToUpper();
        Vector2 size = new Vector2(Mathf.CeilToInt(GetTotalPixelWidth(text) + marginPixel * (text.Length-1)), 
            Mathf.CeilToInt(pFontLoader.charHeightInPixel));
        Texture2D tex = new Texture2D((int)size.x, (int)size.y);

        //Debug.Log("texture size: " + tex.width + " , " + tex.height);
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Point;
        Color[] cols = tex.GetPixels(0,0,tex.width, tex.height);

        int xOffset = 0;

        // Set all pixels alpha to transparent
        for(int i = 0; i < cols.Length; ++i){
            //cols[i].a = 0f;
            cols[i] = Color.white;
        }
        
        for(int i = 0; i < text.Length; ++i){
            PFontLoader.CharInfo charInfo = pFontLoader.chars[text[i]];
            Sprite charSprite = charInfo.sprite;
            Color[] charCols = charSprite.texture.GetPixels((int)charSprite.rect.xMin, (int)charSprite.rect.yMin, (int)charSprite.rect.width, (int)charSprite.rect.height);
            int charWidth = (int)charInfo.width;
            for(int j = 0; j < charCols.Length; ++j){
                int row = Mathf.FloorToInt(j / charWidth);
                int column = xOffset + j%charWidth;
                //Debug.Log("row: " + row + " column: " + column);
                
                cols[row * tex.width + column] = charCols[j];
            }
            xOffset += marginPixel + (int)charInfo.width;
        }
        
        tex.SetPixels(cols);
        tex.Apply();

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), pixelPerUnit);
        displayImage.sprite = sprite;
        displayImage.SetNativeSize();
        //GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
    }
}
