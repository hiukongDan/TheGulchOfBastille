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

    public void SetMiscData(Dictionary<int, int> conversationIndex, Dictionary<int, bool> gateOpened){
        if(this.conversationIndex == null){
            this.conversationIndex = new Dictionary<int, int>();
        }
        else{
            this.conversationIndex.Clear();
        }
        foreach(KeyValuePair<int, int> item in conversationIndex){
            this.conversationIndex.Add(item.Key, item.Value);
        }

        if(this.gateOpened == null){
            this.gateOpened = new Dictionary<int, bool>();
        }
        else{
            this.gateOpened.Clear();
        }
        foreach(KeyValuePair<int, bool> item in gateOpened){
            this.gateOpened.Add(item.Key, item.Value);
        }

    }
}
