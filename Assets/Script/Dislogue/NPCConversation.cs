using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newNPCConversation", menuName = "Data/NPC Conversation/NPC Conversation")]
public class NPCConversation : ScriptableObject
{
    [TextArea(3,3)]
    public string[] conversations;
    public NPCSelection[] selections;
}
