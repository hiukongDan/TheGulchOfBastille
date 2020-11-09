using Gulch;
using UnityEngine;

public class GreyGulchMainEventHandler : MonoBehaviour
{
    public NPCConversation playerSlayTwoSlowMutant;
    private NPC npc;
    void Start()
    {
        GulchMainEventListener.Instance.Slay_SlowMutant_TrainingGround += OnPlayerSlayTwoSlowMutant;
        npc = GetComponent<NPC>();
    }

    public void OnPlayerSlayTwoSlowMutant()
    {
        npc.npcConversationHandler.SetConversation(playerSlayTwoSlowMutant);
        //GetComponent<NPCConversationHandler>().SetConversation(playerSlayTwoSlowMutant);

        GulchMainEventListener.Instance.Slay_SlowMutant_TrainingGround -= OnPlayerSlayTwoSlowMutant;
    }

}
