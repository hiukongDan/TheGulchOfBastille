using System;
using System.Collections.Generic;

[Serializable]
public class MiscData
{
    /// <Summary>
    /// key: NPCConversationHandler.GetInstanceID()
    /// value: index of the conversation
    /// </Summary>
    public static Dictionary<int, int> conversationIndex = new Dictionary<int, int>();

    /// <Summary>
    /// key: Gate instanceID
    /// value: isGateOpened
    /// </Summary>
    public static Dictionary<int, bool> gateOpened = new Dictionary<int, bool>();

    public void Init(){
        conversationIndex.Clear();
        gateOpened.Clear();
    }

    public void SetMiscData(Dictionary<int, int> conversationIndex, Dictionary<int, bool> gateOpened){
        if(conversationIndex == null){
            conversationIndex = new Dictionary<int, int>();
        }
        else{
            conversationIndex.Clear();
        }
        foreach(KeyValuePair<int, int> item in conversationIndex){
            conversationIndex.Add(item.Key, item.Value);
        }

        if(gateOpened == null){
            gateOpened = new Dictionary<int, bool>();
        }
        else{
            gateOpened.Clear();
        }
        foreach(KeyValuePair<int, bool> item in gateOpened){
            gateOpened.Add(item.Key, item.Value);
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
        conversationIndex.Clear();
        for(int i = 0; i < miscSaveData.conversationIndexKey.Count; ++i){
            conversationIndex.Add(miscSaveData.conversationIndexKey[i], miscSaveData.conversatioinIndexValue[i]);
        }

        gateOpened.Clear();
        for(int i = 0; i < miscSaveData.gateOpenedKey.Count; ++i){
            gateOpened.Add(miscSaveData.gateOpenedKey[i], miscSaveData.gateOpenedValue[i]);
        }
    }
}
