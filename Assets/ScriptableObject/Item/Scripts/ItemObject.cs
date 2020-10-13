using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Default, Pickable}
public class ItemObject : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    [TextArea(3, 5)]
    public string description;
    public int amount;

}
