using System;
using System.Collections.Generic;

[Serializable]
public class MiscData
{
    /// <Summary>
    /// key: NPCConversationHandler.GetHashCode()
    /// value: index of the conversation
    /// </Summary>
    public Dictionary<int, int> conversationIndex = new Dictionary<int, int>();

    /// <Summary>
    /// key: Gate hashcode
    /// value: isGateOpened
    /// </Summary>
    public Dictionary<int, bool> gateOpened = new Dictionary<int, bool>();

    public void Init(){
        conversationIndex.Clear();
        gateOpened.Clear();
    }
}
