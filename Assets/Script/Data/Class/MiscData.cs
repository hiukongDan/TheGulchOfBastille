using System;
using System.Collections.Generic;

[Serializable]
public class MiscData
{
    /// <Summary>
    /// key: NPCConversationHandler.GetInstanceID()
    /// value: index of the conversation
    /// </Summary>
    public static Dictionary<string, int> conversationIndex = new Dictionary<string, int>();

    /// <Summary>
    /// key: Gate instanceID
    /// value: isGateOpened
    /// </Summary>
    public static Dictionary<string, bool> gateOpened = new Dictionary<string, bool>();

    public void Init(){
        conversationIndex.Clear();
        gateOpened.Clear();
    }

    public void SetMiscData(Dictionary<string, int> conversationIndex, Dictionary<string, bool> gateOpened){
        if(conversationIndex == null){
            conversationIndex = new Dictionary<string, int>();
        }
        else{
            conversationIndex.Clear();
        }
        foreach(KeyValuePair<string, int> item in conversationIndex){
            conversationIndex.Add(item.Key, item.Value);
        }

        if(gateOpened == null){
            gateOpened = new Dictionary<string, bool>();
        }
        else{
            gateOpened.Clear();
        }
        foreach(KeyValuePair<string, bool> item in gateOpened){
            gateOpened.Add(item.Key, item.Value);
        }
    }

    [Serializable]
    public struct MiscSaveData{
        public List<string> conversationIndexKey;
        public List<int> conversatioinIndexValue;
        public List<string> gateOpenedKey;
        public List<bool> gateOpenedValue;

        public MiscSaveData(Dictionary<string, int> conversationIndex, Dictionary<string, bool> gateOpened){
            conversationIndexKey = new List<string>();
            conversatioinIndexValue = new List<int>();
            foreach(KeyValuePair<string, int> item in conversationIndex){
                conversationIndexKey.Add(item.Key);
                conversatioinIndexValue.Add(item.Value);
            }
            
            gateOpenedKey = new List<string>();
            gateOpenedValue = new List<bool>();
            foreach(KeyValuePair<string, bool> item in gateOpened){
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
