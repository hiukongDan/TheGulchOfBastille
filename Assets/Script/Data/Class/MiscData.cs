using System;
using System.Collections.Generic;

[Serializable]
public class MiscData
{
    public bool isAbandonedDoorOpen = false;
    public Dictionary<int, int> conversationIndex = new Dictionary<int, int>();

    public void Init(){
        isAbandonedDoorOpen = false;
        conversationIndex.Clear();
    }
}
