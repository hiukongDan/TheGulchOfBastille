using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName="newPickableItem", menuName="Data/Item/Pickable Item")]
public class PickableItem : ItemObject
{
    public PickableItem()
    {
        itemType = ItemType.Pickable;
    }
}
