using UnityEngine;

public class NPCConversationHandler : MonoBehaviour
{
    public PFontInfo pFontInfo;

    public Animator InfoSignAnim;
    public Animator DialogueBoxAnim;

    public NPCConversation npcConversation;

    private NPC npc;
    private PFontLoader pFontLoader;

    void Start()
    {
        npc = GetComponentInParent<NPC>();
        pFontLoader = new PFontLoader(pFontInfo);
    }

    void Update()
    {
        
    }

    // ---- Info Sign -----------------------------------
    public void OnEnterInteractionArea()
    {
        InfoSignAnim.Play(InfoSignAnimHash.INTRO);
    }

    public void OnExitInteractionArea()
    {
        InfoSignAnim.Play(InfoSignAnimHash.OUTRO);
    }

    public void OnBeginInteraction()
    {
        InfoSignAnim.Play(InfoSignAnimHash.EMPTY);
        DialogueBoxAnim.Play(DialogueBoxAnimHash.IDLE);
    }
    // --------------------------------------------------

    public void OnInteraction()
    {

    }

    
    public class InfoSignAnimHash
    {
        public static int IDLE = Animator.StringToHash("idle");
        public static int INTRO = Animator.StringToHash("intro");
        public static int OUTRO = Animator.StringToHash("outro");
        public static int EMPTY = Animator.StringToHash("empty");
    }
    public class DialogueBoxAnimHash
    {
        public static int IDLE = Animator.StringToHash("idle");
        public static int EMPTY = Animator.StringToHash("empty");
    }
}
