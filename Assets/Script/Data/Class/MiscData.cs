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

    [Serializable]
    public struct MiscSaveData{
        public List<int> conversationIndexKey;
        public List<int> conversatioinIndexValue;
        public List<int> gateOpenedKey;
        public List<bool> gateOpenedValue;

        public MiscSaveData(Dictionary<int, int> conversationIndex, Dictionary<int, bool> gateOpened){
            conversationIndexKey = new List<int>();
            conversatioinIndexValue = new List<int>();
            foreach(KeyValuePair<int, int> item in conversationIndex){
                conversationIndexKey.Add(item.Key);
                conversatioinIndexValue.Add(item.Value);
            }
            
            gateOpenedKey = new List<int>();
            gateOpenedValue = new List<bool>();
            foreach(KeyValuePair<int, bool> item in gateOpened){
                gateOpenedKey.Add(item.Key);
                gateOpenedValue.Add(item.Value);
            }
        }
    }

    public MiscSaveData GetMiscSaveData(){
        return new MiscSaveData(conversationIndex, gateOpened);
    }

    public void SetMiscSaveData(MiscSaveData miscSaveData){
        this.conversationIndex.Clear();
        for(int i = 0; i < miscSaveData.conversationIndexKey.Count; ++i){
            conversationIndex.Add(miscSaveData.conversationIndexKey[i], miscSaveData.conversatioinIndexValue[i]);
        }

        this.gateOpened.Clear();
        for(int i = 0; i < miscSaveData.gateOpenedKey.Count; ++i){
            gateOpened.Add(miscSaveData.gateOpenedKey[i], miscSaveData.gateOpenedValue[i]);
        }
    }
}
