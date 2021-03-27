using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NPCConversationHandler : MonoBehaviour
{
    public PFontInfo pFontInfo;
    public Animator InfoSignAnim;
    public Animator DialogueBoxAnim;
    public List<NPCConversation> npcConversations;
    private NPCConversation npcConversation;
    private int currentConversationIndex = 0;
    public float letterPerLine = 15f;
    public float pixelPerUnit = 32f;
    public Vector2 borderInPixel = new Vector2(2, 2);
    public Vector2 marginInPixel = new Vector2(1, 1);
    public float textSpeed = 4f;
    
    private NPC npc;
    private Player player;

    private Transform charOrigin;
    private Transform selectionBox;

    private PFontLoader pFontLoader;

    private Vector2 originPoint;
    private Vector2 boxSize;

    private ObjectPool OP;

    private Queue<char> nextSentence;
    private List<NPCSelection> npcSelections;

    private float totalTime;
    private Vector2 lastPos;
    private Vector2 selectionBoxSize;
    private int currentSelection;

    private List<GameObject> recycleObject;
    private List<CharInfoWrapper> wordObject;

    void Awake()
    {
        npc = GetComponentInParent<NPC>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pFontLoader = new PFontLoader(pFontInfo);
        OP = new ObjectPool();
        charOrigin = DialogueBoxAnim.gameObject.transform.GetChild(0);
        selectionBox = DialogueBoxAnim.gameObject.transform.GetChild(1);

        recycleObject = new List<GameObject>();
        nextSentence = new Queue<char>();
        wordObject = new List<CharInfoWrapper>();
        npcSelections = new List<NPCSelection>();
    }

    void OnEnable(){
        if(player && player.miscData.conversationIndex.ContainsKey(GetHashCode())){
            currentConversationIndex = player.miscData.conversationIndex[GetHashCode()];
        }
        else if(player){
            currentConversationIndex = 0;
            player.miscData.conversationIndex.Add(GetHashCode(), 0);
        }

        if(npcConversations.Count() > 0){
            npcConversation = npcConversations[currentConversationIndex];
        }
    }

    void OnDisable() {
        OP.DestroyGameObject();    
    }

    void Start()
    {
        
    }

    // ----- DIALOGUBE BOX ---------------------------------------------------------------------------
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

    // --------------------------------------------------------------------------------

    // ----- SELECTION BOX ------------------------------------------------------------
    private bool initSelectionBox()
    {
        npcSelections = npcConversation.GetSelections();
        if (npcSelections.Count == 0)
            return false;

        // calculate size
        float length = 0;
        float charCount = 0;

        foreach(NPCSelection npcSelection in npcSelections)
        {
            if (length < pFontLoader.GetLengthInPixel(npcSelection.selection))
            {
                length = pFontLoader.GetLengthInPixel(npcSelection.selection);
                charCount = npcSelection.selection.Length;
            }
        }

        float height = npcSelections.Count;

        selectionBoxSize = new Vector2((borderInPixel.x * 2 + (length + marginInPixel.x * charCount)) / pixelPerUnit,
            (borderInPixel.y * 2 + height * pFontLoader.charHeightInPixel + (height - 1) * marginInPixel.y) / pixelPerUnit);

        selectionBox.GetComponent<SpriteRenderer>().size = selectionBoxSize;

        // calculate position
        Vector2 boxPos = new Vector2(this.boxSize.x / 2 + selectionBoxSize.x / 2 + borderInPixel.x / pixelPerUnit,
            0f);

        selectionBox.localPosition = boxPos;

        setSelectionIconPos(0, selectionBoxSize);

        // collider
        var oldSize = selectionBox.GetComponent<BoxCollider>().size;
        selectionBox.GetComponent<BoxCollider>().size = new Vector3(selectionBoxSize.x, selectionBoxSize.y, oldSize.z);

        // set active
        selectionBox.gameObject.SetActive(true);

        // set current selection
        currentSelection = 0;

        // reset selections items
        Vector2 startPoint = new Vector2(-selectionBoxSize.x / 2 + borderInPixel.x / pixelPerUnit, selectionBoxSize.y / 2 - (borderInPixel.y + pFontLoader.charHeightInPixel/2) / pixelPerUnit);

        lastPos = startPoint;

        for (int i = 0; i < npcSelections.Count; i++)
        {
            List<char> chars = npcSelections[i].selection.ToList();

            foreach(char ch in chars)
            {
                if (char.IsWhiteSpace(ch))
                    lastPos.Set(lastPos.x + pFontLoader.charWidthInPixel / pixelPerUnit, lastPos.y);

                ShowLetter(ch, lastPos, selectionBox.transform);
            }
            lastPos.Set(-selectionBoxSize.x / 2 + borderInPixel.x / pixelPerUnit, lastPos.y - (marginInPixel.y + pFontLoader.charHeightInPixel) / pixelPerUnit);
        }

        return true;
    }

    private int setSelectionIconPos(int index, Vector2 boxSize)
    {
        if (index < 0 || index >= npcSelections.Count)
            return -1;

        Vector2 iconPos = new Vector2(boxSize.x / 2 + (marginInPixel.x * 4) / pixelPerUnit, boxSize.y / 2 - (borderInPixel.y + pFontLoader.charHeightInPixel / 2 + (marginInPixel.y + pFontLoader.charHeightInPixel) * index) / pixelPerUnit);

        selectionBox.GetChild(0).localPosition = iconPos;

        return index;
    }

    // --------------------------------------------------------------------------------

    private enum ConversationStatus
    {
        EMPTY, CONVERSE, SELECTION,
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
            case ConversationStatus.SELECTION:
                onSelection();
                break;
            default:
                break;
        }
    }

    private void onConverse()
    {
        if(nextSentence.Count == 0)
        {
            if (!npcConversation.HasNext() && initSelectionBox())
            {
                _currentConverseStatus = ConversationStatus.SELECTION;
            }
            else
            {
                _currentConverseStatus = ConversationStatus.EMPTY;
                resetTotalTime();
            }
            return;
        }

        int count = (int)(totalTime) - recycleObject.Count;
        for (int i = 0; i < count; i++)
        {
            if (!digestNextLetter())
            {
                if (!npcConversation.HasNext() && initSelectionBox())
                {
                    _currentConverseStatus = ConversationStatus.SELECTION;
                }
                else
                {
                    _currentConverseStatus = ConversationStatus.EMPTY;
                    resetTotalTime();
                }
                break;
            }
        }
        totalTime += Time.deltaTime * textSpeed;
    }

    private void onSelection()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(player.InputHandler.MousePosInput);
        if(Physics.Raycast(ray, out hit, LayerMask.NameToLayer("Selectable")))
        {
            var localPos = selectionBox.InverseTransformPoint(hit.point);
            int index = npcSelections.Count - Mathf.FloorToInt((localPos.y + selectionBoxSize.y / 2) / (selectionBoxSize.y / npcSelections.Count)) - 1;
            if(currentSelection != index)
            {
                selectItem(index);
            }
        }
    }

    private void selectItem(int index)
    {
        if (index < 0 || index >= npcSelections.Count)
            return;

        if(index != currentSelection)
        {
            currentSelection = index;
            setSelectionIconPos(index, selectionBoxSize);
        }
    }

    private void confirmSelection()
    {
        if (_currentConverseStatus != ConversationStatus.SELECTION)
            return;

        NPCSelection selection = npcSelections[currentSelection];
        npcConversation = selection.conversation;

        if(npcConversation != null)
            npcConversation.ResetIndex();

        selectionBox.gameObject.SetActive(false);

        _currentConverseStatus = ConversationStatus.EMPTY;
        OnInteraction();
    }

    private void resetTotalTime() => totalTime = 0f;

    // ---- DELEGATE HANDLER -----------------------------------
    public void OnEnterInteractionArea()
    {
        InfoSignAnim?.Play(InfoSignAnimHash.INTRO);

        selectionBox.gameObject.SetActive(false);

        // npcConversation = npcConversationRandom;
    }

    public void OnExitInteractionArea()
    {
        InfoSignAnim?.Play(InfoSignAnimHash.OUTRO);
    }

    public void OnBeginInteraction()
    {
        npc.npcEventHandler.NPCSelection += DialogueSelectionHandler;

        InfoSignAnim?.Play(InfoSignAnimHash.EMPTY);
        DialogueBoxAnim.Play(DialogueBoxAnimHash.IDLE);
        initBox();

        if (npcConversation == null){
            nextSentence = new Queue<char>("...");
            resetTotalTime();
            _currentConverseStatus = ConversationStatus.CONVERSE;
            
            // npcConversation = npcConversationRandom;
        }

        if(npcConversation != null)
        {
            if(npcConversation.isRandomConversation){
                nextSentence = new Queue<char>(npcConversation.GetRandomSentence());
            }
            else{
                npcConversation.ResetIndex();
                getNextSentence();
            }

            resetTotalTime();
            _currentConverseStatus = ConversationStatus.CONVERSE;
        }
        else
        {
            OnEndInteraction();
        }
    }

    public void OnEndInteraction()
    {
        cleanUpBox();
        DialogueBoxAnim.Play(DialogueBoxAnimHash.EMPTY);
        InfoSignAnim?.Play(InfoSignAnimHash.INTRO);

        OP.DestroyGameObject();

        _currentConverseStatus = ConversationStatus.EMPTY;

        // Change conversation default to next conversation if presented
        if(npcConversation != null && currentConversationIndex + 1 < npcConversations.Count())
        {
            currentConversationIndex++;
            SetConversation(npcConversations[currentConversationIndex]);

            if(player.miscData.conversationIndex.ContainsKey(GetHashCode())){
                player.miscData.conversationIndex[GetHashCode()] = currentConversationIndex;
            }
            else{
                player.miscData.conversationIndex.Add(GetHashCode(), currentConversationIndex);
            }
            SetConversation(npcConversation);
        }

        npc.npcEventHandler.NPCSelection -= DialogueSelectionHandler;
    }


    // direction : positive - up, negative - down
    public void DialogueSelectionHandler(int direction)
    {
        if (_currentConverseStatus != ConversationStatus.SELECTION)
            return;

        if(direction > 0)
        {
            selectItem(currentSelection - 1);
        }
        else if(direction < 0)
        {
            selectItem(currentSelection + 1);
        }
    }
    // --------------------------------------------------

    public void OnInteraction()
    {
        switch (_currentConverseStatus)
        {
            case ConversationStatus.EMPTY:
                // seek next converse or question
                if (npcConversation != null && npcConversation.HasNext())
                {
                    getNextSentence();
                    cleanUpBox();
                    _currentConverseStatus = ConversationStatus.CONVERSE;
                }
                else
                {
                    npc.npcEventHandler.OnNPCEndInteraction();
                }
                break;
            case ConversationStatus.CONVERSE:
                // skip converse
                totalTime = 100f;
                break;
            case ConversationStatus.SELECTION:
                confirmSelection();
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

    // dialgoue box
    private CharInfoWrapper ShowLetter(char ch, Vector2 lastPos, Transform charOrigin)
    {
        if (char.IsWhiteSpace(ch))
            return null;

        PFontLoader.CharInfo ci = pFontLoader.chars[char.ToUpper(ch)];
        var go = OP.GetItem();
        var sr = go.GetComponent<SpriteRenderer>();

        CharInfoWrapper ciw = new CharInfoWrapper(ci, go);

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

        this.lastPos.Set(lastPos.x + ci.width / 2 / pixelPerUnit, lastPos.y);

        return ciw;
    }

    private Vector2 getNextPos(Vector2 prevPos, PFontLoader.CharInfo ci)
    {
        if (prevPos.x + (marginInPixel.x + ci.width) / pixelPerUnit > boxSize.x / 2 - borderInPixel.x / pixelPerUnit)
        {
            IncreaseLine();
            prevPos.Set(originPoint.x - marginInPixel.x / pixelPerUnit, prevPos.y - (marginInPixel.y + pFontLoader.charHeightInPixel) / pixelPerUnit);
            int len = wordObject.Count;
            for (int i = 0; i < len; i++)
            {
                prevPos.Set(prevPos.x + (marginInPixel.x + wordObject[i].ci.width / 2) / pixelPerUnit, prevPos.y);
                wordObject[i].GO.transform.localPosition = prevPos;
                prevPos.Set(prevPos.x + wordObject[i].ci.width / 2 / pixelPerUnit, prevPos.y);
            }
        }
        prevPos.Set(prevPos.x + (marginInPixel.x + ci.width / 2) / pixelPerUnit, prevPos.y);

        return prevPos;
    }

    private void IncreaseLine()
    {
        float yOffset = (pFontLoader.charHeightInPixel + marginInPixel.y) / pixelPerUnit;

        SpriteRenderer sr = DialogueBoxAnim.gameObject.GetComponent<SpriteRenderer>();
        boxSize = sr.size = new Vector2(boxSize.x, boxSize.y + yOffset);

        // Reset dialogue box position
        DialogueBoxAnim.transform.localPosition = new Vector2(DialogueBoxAnim.transform.localPosition.x, DialogueBoxAnim.transform.localPosition.y + yOffset / 2);

        charOrigin.localPosition = new Vector2(charOrigin.localPosition.x, charOrigin.localPosition.y + yOffset / 2);
    }

    // ---------- WORDS PARSER ----------------------------------------------------------------
    public bool digestNextLetter()
    {
        if (nextSentence.Count == 0)
            return false;

        char ch = nextSentence.Dequeue();
        bool isletter = false;
        CharInfoWrapper ciw = null;
        if (char.IsWhiteSpace(ch))
        {
            lastPos.Set(lastPos.x + pFontLoader.charWidthInPixel/pixelPerUnit, lastPos.y);
            wordObject.Clear();
        }
        else if (char.IsLetter(ch))
        {
            ciw = ShowLetter(ch, lastPos, charOrigin);
            isletter = true;
        }
        else if (pFontLoader.chars.Keys.Contains(ch)){
            ciw = ShowLetter(ch, lastPos, charOrigin);
            isletter = true;
        }
        else
        {
            throw new InvalidDataException();
        }

        if (isletter)
        {
            wordObject.Add(ciw);
        }
        
        return true;
    }

    private class CharInfoWrapper
    {
        public PFontLoader.CharInfo ci { get; private set; }
        public GameObject GO { get; private set; }

        public CharInfoWrapper(PFontLoader.CharInfo ci, GameObject GO)
        {
            this.ci = ci;
            this.GO = GO;
        }
    }

    public void SetConversation(NPCConversation npcConversation)
    {
        if(npcConversation != null)
        {
            this.npcConversation = npcConversation;
        }
    }

}
