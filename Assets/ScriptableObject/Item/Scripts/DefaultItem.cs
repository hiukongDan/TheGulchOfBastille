using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newDefaultItem", menuName="Data/Item/Default Item")]
public class DefaultItem : ItemObject
{
    public DefaultItem()
    {
        itemType = ItemType.Default;
    }
}
