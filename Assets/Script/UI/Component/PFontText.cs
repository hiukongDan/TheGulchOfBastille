using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class PFontText : MonoBehaviour
{
    public PFontInfo pFontInfo;
    public float pixelPerUnit = 32f;

    [TextArea(5,5)]
    public string text;

    public bool isCentered = false;

    [Range(0, 10)]
    public int marginPixel = 1;
    [Range(0, 10)]
    public int marginPixelVert = 0;

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

    //  void OnValidate(){
    //     OnTextChange();
    //  }

    public void OnTextChange(){
        if(text.Length == 0)
            return;

        pFontLoader = new PFontLoader(pFontInfo);
        displayImage = GetComponent<Image>();

        text = text.ToUpper();
        string[] sentences = text.Split('\n');
        int length = sentences.Length;
        // Debug.Log("Length" + length);
        Vector2 totalSize = new Vector2(Mathf.CeilToInt(getLongestPixelStringWidth(sentences) + marginPixel * (text.Length-1)),
            Mathf.CeilToInt((pFontLoader.charHeightInPixel + marginPixelVert)*length - marginPixelVert));
        Texture2D finalTex = new Texture2D((int)totalSize.x, (int)totalSize.y);
        finalTex.wrapMode = TextureWrapMode.Clamp;
        finalTex.filterMode = FilterMode.Point;
        Color[] finalCols = finalTex.GetPixels(0, 0, finalTex.width, finalTex.height);
        for(int i = 0; i < finalCols.Length; ++i){
            finalCols[i].a = 0.0f;
        }

        // parse each sentence
        int yOffset = finalTex.height - (int)pFontLoader.charHeightInPixel;
        foreach(string text in sentences){
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
                cols[i].a = 0f;
                // cols[i] = Color.white;
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
            
            //tex.SetPixels(cols);
            //tex.Apply();

            // apply texture to final texture
            int beginOffset = yOffset * finalTex.width;
            int beginXOffset = 0;
            if(isCentered){
                beginXOffset = Mathf.FloorToInt((finalTex.width - tex.width)/2);
            }
            //Color[] texCols = tex.GetPixels(0, 0, tex.width, tex.height);
            for(int i = 0; i < cols.Length; ++i){
                int finalIndex = beginOffset + Mathf.FloorToInt(i/tex.width)*finalTex.width + beginXOffset + i%tex.width;
                finalCols[finalIndex] = cols[i];
            }

            yOffset -= (int)(pFontLoader.charHeightInPixel + marginPixelVert);
        }

        finalTex.SetPixels(finalCols);
        finalTex.Apply();

        Sprite sprite = Sprite.Create(finalTex, new Rect(0, 0, finalTex.width, finalTex.height), new Vector2(0.5f, 0.5f), pixelPerUnit);
        displayImage.sprite = sprite;
        displayImage.SetNativeSize();
        //GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
    }

    private int getLongestPixelStringWidth(string[] strs){
        int ret = 0;
        foreach(string str in strs){
            int tmp = GetTotalPixelWidth(str);
            if(tmp > ret){
                ret = tmp;
            }
        }
        return ret;
    }
}
