using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newInventory", menuName="Data/Item Inventory/Inventory")]
public class InventoryObject : ScriptableObject//, ISerializationCallbackReceiver
{
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    public Dictionary<ItemObject, int> GetID = new Dictionary<ItemObject, int>();



/*    public void OnAfterDeserialize()
    {
        throw new System.NotImplementedException();


    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }*/
}


