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

    private int index;
    public void ResetIndex() => index = 0;
    public bool HasNext() => index < conversations.Length;
    public List<char> GetNextSentence()
    {
        return conversations[index++].ToList();
    }

    public List<NPCSelection> GetSelections()
    {
        return selections.ToList();
    }
}
