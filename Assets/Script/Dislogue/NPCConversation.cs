using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "newNPCConversation", menuName = "Data/NPC Conversation/NPC Conversation")]
public class NPCConversation : ScriptableObject
{
    [TextArea(3, 3)]
    public string[] conversations;
    public NPCSelection[] selections;
    public NPCConversation nextConversation;
    public bool isRandomConversation = false;

    private int index;
    public void ResetIndex() => index = 0;
    public bool HasNext() => index < conversations.Length && !isRandomConversation;

    public List<char> GetRandomSentence(){
        return conversations[Mathf.FloorToInt(Random.value * conversations.Count())].ToList<char>();
    }
    public List<char> GetNextSentence()
    {
        return conversations[index++].ToList();
    }

    public List<NPCSelection> GetSelections()
    {
        return selections.ToList();
    }
}
