using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newPFontInfo", menuName="Data/PFont/PFont Info")]
public class PFontInfo : ScriptableObject
{
    public string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789?!,.";
    public string path = "PFont/text_0";
}
