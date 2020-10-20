using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFontLoader
{
    private static Dictionary<PFontInfo, PFontLoader> pFontDict;

    public static Dictionary<PFontInfo, PFontLoader> PFontDict
    {
        get
        {
            if(pFontDict == null)
            {
                pFontDict = new Dictionary<PFontInfo, PFontLoader>();
            }
            return pFontDict;
        }
    }

    public Dictionary<char, CharInfo> chars { get; private set; }

    public PFontLoader(PFontInfo pfontInfo)
    {
        if (pfontInfo == null)
            throw new ArgumentNullException();

        PFontLoader pFontLoader;
        PFontDict.TryGetValue(pfontInfo, out pFontLoader);
        if(pFontLoader != null)
        {
            chars = pFontLoader.chars;
        }
        else
        {
            initDictionary(pfontInfo);
            PFontDict[pfontInfo] = this;
        }
    }

    private void initDictionary(PFontInfo pfontInfo)
    {
        chars = new Dictionary<char, CharInfo>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(pfontInfo.path);

        Char[] chs = pfontInfo.characters.ToCharArray();
        int i = 0;

        foreach(char ch in chs)
        {
            CharInfo ci = new CharInfo(ch, sprites[i], sprites[i].rect.width);
            chars[ch] = ci;

            i++;
        }
    }

    public class CharInfo
    {
        public float width { get; private set; }
        public Sprite sprite { get; private set; }
        public char ch;

        public CharInfo(char ch, Sprite sprite, float width)
        {
            this.ch = ch;
            this.sprite = sprite;
            this.width = width;
        }
    }
}

