using System;
using System.Collections.Generic;

[Serializable]
public class MiscData
{
    public bool isAbandonedDoorOpen = false;

    /// <Summary>
    /// key: NPCConversationHandler.GetHashCode()
    /// value: index of the conversation
    /// </Summary>
    public Dictionary<int, int> conversationIndex = new Dictionary<int, int>();

    public void Init(){
        isAbandonedDoorOpen = false;
        conversationIndex.Clear();
    }
}
