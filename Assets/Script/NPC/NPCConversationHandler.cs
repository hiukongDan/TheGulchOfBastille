using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class NPCConversationHandler : MonoBehaviour
{
    public PFontInfo pFontInfo;

    public Animator InfoSignAnim;
    public Animator DialogueBoxAnim;

    public NPCConversation npcConversation;

    public float letterPerLine = 15f;
    public float pixelPerUnit = 32f;
    public Vector2 borderInPixel = new Vector2(2, 2);
    public Vector2 marginInPixel = new Vector2(1, 1);

    public float textSpeed = 4f;
    
    private NPC npc;

    private Transform charOrigin;

    private PFontLoader pFontLoader;

    private Vector2 originPoint;
    private Vector2 boxSize;

    private ObjectPool OP;

    private Queue<char> nextSentence;

    private float totalTime;
    private Vector2 lastPos;

    private List<GameObject> recycleObject;
    private List<PFontLoader.CharInfo> wordObject;

    void Start()
    {
        npc = GetComponentInParent<NPC>();
        pFontLoader = new PFontLoader(pFontInfo);
        OP = new ObjectPool();
        charOrigin = DialogueBoxAnim.gameObject.transform.GetChild(0);
        recycleObject = new List<GameObject>();
        nextSentence = new Queue<char>();
        wordObject = new List<PFontLoader.CharInfo>();
    }

    private void initBox() => initOriginPoint(initBoxSize());

    private Vector2 initBoxSize()
    {
        GameObject go = DialogueBoxAnim.gameObject;
        SpriteRenderer sp = go.GetComponent<SpriteRenderer>();
     
        Vector2 size = new Vector2();
        size.x = (letterPerLine * (pFontLoader.charWidthInPixel + marginInPixel.x) + borderInPixel.x * 2) / pixelPerUnit;
        size.y = (pFontLoader.charHeightInPixel + borderInPixel.y * 2) / pixelPerUnit;

        boxSize = sp.size = size;

        return size;
    }

    private Vector2 initOriginPoint(Vector2 boxSize)
    {
        originPoint.Set(-boxSize.x / 2 + (borderInPixel.x + marginInPixel.x) / pixelPerUnit, 0f);
        lastPos.Set(originPoint.x-marginInPixel.x/pixelPerUnit, originPoint.y);
        return originPoint;
    }

    private enum ConversationStatus
    {
        EMPTY, CONVERSE, QUESTION,
    }

    private ConversationStatus _currentConverseStatus = ConversationStatus.EMPTY;

    void Update() 
    {
        switch (_currentConverseStatus)
        {
            case ConversationStatus.EMPTY:
                break;
            case ConversationStatus.CONVERSE:
                onConverse();
                break;
            case ConversationStatus.QUESTION:
                onQuestion();
                break;
            default:
                break;
        }
    }

    private void onConverse()
    {
        if(nextSentence.Count == 0)
        {
            _currentConverseStatus = ConversationStatus.EMPTY;
            resetTotalTime();
            return;
        }

        int count = (int)(totalTime) - recycleObject.Count;
        for (int i = 0; i < count; i++)
        {
            if (!digestNextLetter())
            {
                _currentConverseStatus = ConversationStatus.EMPTY;
                resetTotalTime();
                break;
            }
        }
        totalTime += Time.deltaTime * textSpeed;
    }

    private void onQuestion()
    {

    }

    private void resetTotalTime() => totalTime = 0f;

    // ---- DELEGATE HANDLER -----------------------------------
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
        initBox();
        npcConversation.ResetIndex();
        getNextSentence();
        resetTotalTime();

        _currentConverseStatus = ConversationStatus.CONVERSE;
    }

    public void OnEndInteraction()
    {
        cleanUpBox();
        DialogueBoxAnim.Play(DialogueBoxAnimHash.EMPTY);
        InfoSignAnim.Play(InfoSignAnimHash.INTRO);

        OP.DestroyGameObject();
    }
    // --------------------------------------------------



    public void OnInteraction()
    {
        switch (_currentConverseStatus)
        {
            case ConversationStatus.EMPTY:
                // seek next converse or question
                if (npcConversation.HasNext())
                {
                    getNextSentence();
                    cleanUpBox();
                    _currentConverseStatus = ConversationStatus.CONVERSE;
                }
                // TODO: else if selection:
                else
                {
                    npc.npcEventHandler.OnNPCEndInteraction();
                }
                break;
            case ConversationStatus.CONVERSE:
                // skip converse
                totalTime = 100f;
                break;
            case ConversationStatus.QUESTION:
                // give next converse or return player control
                break;
            default:
                break;
        }
    }

    private void cleanUpBox()
    {
        // reset position and size
        DialogueBoxAnim.gameObject.transform.localPosition = Vector2.zero;
        initBox();

        // reset charOrigin point
        charOrigin.localPosition = Vector2.zero;

        // return gameobject
        // set gameObject to false
        foreach (GameObject go in recycleObject)
        {
            go.SetActive(false);
            OP.ReturnItem(go);
        }

        // reset total time
        resetTotalTime();

        // clear collections
        recycleObject.Clear();
        wordObject.Clear();
    }

    private void getNextSentence() => nextSentence = new Queue<char>(npcConversation.GetNextSentence());

    private PFontLoader.CharInfo ShowLetter(char ch)
    {
        PFontLoader.CharInfo ci = pFontLoader.chars[char.ToUpper(ch)];
        var go = OP.GetItem();
        var sr = go.GetComponent<SpriteRenderer>();
        

        if(sr == null)
        {
            sr = go.AddComponent<SpriteRenderer>();
            sr.sortingLayerID = DialogueBoxAnim.gameObject.GetComponent<SpriteRenderer>().sortingLayerID;
            sr.sortingOrder = 1;
        }
        go.transform.SetParent(charOrigin);

        lastPos = getNextPos(lastPos, ci);
        go.transform.localPosition = lastPos;
        go.SetActive(true);
        recycleObject.Add(go);
        sr.sprite = ci.sprite;

        lastPos.Set(lastPos.x + ci.width / 2 / pixelPerUnit, lastPos.y);

        return ci;
    }

    // ---------- WORDS PARSER ----------------------------------------------------------------
    public bool digestNextLetter()
    {
        if (nextSentence.Count == 0)
            return false;

        char ch = nextSentence.Dequeue();
        bool isletter = false;
        PFontLoader.CharInfo ci = null;
        if (char.IsWhiteSpace(ch))
        {
            lastPos.Set(lastPos.x + pFontLoader.charWidthInPixel/pixelPerUnit, lastPos.y);
        }
        else if (char.IsLetter(ch))
        {
            ci = ShowLetter(ch);
            isletter = true;
        }
        else if (pFontLoader.chars.Keys.Contains(ch)){
            ci = ShowLetter(ch);
        }
        else
        {
            throw new InvalidDataException();
        }

        if (isletter)
        {
            if(ci != null)
                wordObject.Add(ci);
        }
        else
        {
            wordObject.Clear();
        }
        
        return true;
    }

    private Vector2 getNextPos(Vector2 prevPos, PFontLoader.CharInfo ci)
    {
        if (prevPos.x + (marginInPixel.x + ci.width) / pixelPerUnit > boxSize.x / 2 - (borderInPixel.x + marginInPixel.x) / pixelPerUnit)
        {
            IncreaseLine();
            prevPos.Set(originPoint.x + ci.width/2/pixelPerUnit, prevPos.y - (marginInPixel.y + pFontLoader.charHeightInPixel) / pixelPerUnit);
            int len = wordObject.Count;
            for (int i = 0;i < len; i++)
            {
                // TODO:
            }
        }
        else
        {
            prevPos.Set(prevPos.x + (marginInPixel.x + ci.width/2) / pixelPerUnit, prevPos.y);
        }
        return prevPos;
    }

    private void IncreaseLine()
    {
        float yOffset = (pFontLoader.charHeightInPixel + marginInPixel.y) / pixelPerUnit;

        SpriteRenderer sr = DialogueBoxAnim.gameObject.GetComponent<SpriteRenderer>();
        boxSize = sr.size = new Vector2(boxSize.x, boxSize.y + yOffset);

        // Reset dialogue box position
        DialogueBoxAnim.transform.localPosition = new Vector2(DialogueBoxAnim.transform.localPosition.x, DialogueBoxAnim.transform.localPosition.y + yOffset/2);

        charOrigin.localPosition = new Vector2(charOrigin.localPosition.x, charOrigin.localPosition.y + yOffset / 2);
    }

    // -------- DATA CLASS AND AUXILIARY FUNCTIONS ----------------------------
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
