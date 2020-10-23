using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newNPCSelection", menuName = "Data/NPC Conversation/NPC Selection")]
public class NPCSelection : ScriptableObject
{
    public string selection = "Yes";
    public NPCConversation conversation;
}
