using UnityEngine;
using System.Collections;

public class AbandonedDoor : MonoBehaviour, IInteractable
{
    public Sprite abandonedDoorOpened;
    public Sprite abandonedDoorClosed;
    public Sprite ivy_0;
    public Sprite ivy_1;
    public GameObject areaTransition;
    public GameObject doorOpenTrigger;

    public float cameraShakeSpeed = 1f;
    public float cameraShakeRange = 0.1f;
    public float ivyFallSpeed = 0.2f;
    public float ivyFadeSpeed = 0.2f;

    private GameObject alive;
    private GameObject ivy;
    private GameObject infoSign;

    private bool isInited = false;
    private Player player;

    void Awake(){
        alive = transform.Find("Alive").gameObject;
        ivy = transform.Find("Leaf").gameObject;
        infoSign = transform.Find("InfoSign").gameObject;
        alive.GetComponent<SpriteRenderer>().sprite = abandonedDoorClosed;
    }

    void OnEnable() {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player == null){
            return;
        }
        
        if(!player.miscData.gateOpened.ContainsKey(GetHashCode())){
            player.miscData.gateOpened.Add(GetHashCode(), false);
        }
        if(player.miscData.gateOpened[GetHashCode()] && !isInited){
            InitOpenedDoor();
        }
    }

    void InitOpenedDoor(){
        ivy.SetActive(false);
        alive.GetComponent<SpriteRenderer>().sprite = abandonedDoorOpened;
        alive.GetComponent<Animator>().enabled = false;
        areaTransition.SetActive(true);
        doorOpenTrigger.SetActive(false);
        infoSign.SetActive(false);
        isInited = true;
    }


    public void OnInteraction(){
        if(player.playerRuntimeData.playerStock.Contains(ItemData.KeyItem.Abandoned_Door_Key)){
            infoSign?.GetComponentInChildren<Animator>().Play(InfoSignAnimHash.OUTRO);
            StartCoroutine(OpenAbandonedDoor());
            player.SetInteractable(null);
        }
        else{
            UIEventListener.Instance.OnInfomationChange(new UIEventListener.InfomationChangeData("The Door closed firmly"));
            player.InputHandler.ResetAll();
        }

    }

    public void EnterInteractionArea(){
        infoSign?.GetComponentInChildren<Animator>().Play(InfoSignAnimHash.INTRO);
        player.SetInteractable(this);
    }

    public void ExitInteractionArea(){
        infoSign?.GetComponentInChildren<Animator>().Play(InfoSignAnimHash.OUTRO);
        player.SetInteractable(null);
    }

    IEnumerator OpenAbandonedDoor(){
        /*
        0,   default door+ivy_0
        1,   default door+ivy_1
            *earth quake begin*
        2,   less than 1 sec
        3,   *earth quake end*
            *ivy fall and fade*
        4,   2 or 3 sec?
        5,  *play door anim* 
            *earth quake begin*
            *generate dust particle*(3 types of dust. bigger the sprite be, faster it falls)(dust only exists in the door)
        6,   *finish door anim*
            *earth quake end*
            *stop generating particle*
        */

        player.stateMachine.SwitchState(player.cinemaState);
        if(player.miscData.gateOpened.ContainsKey(GetHashCode())){
            player.miscData.gateOpened[GetHashCode()] = true;
        }
        else{
            player.miscData.gateOpened.Add(GetHashCode(), true);
        }

        ivy.GetComponent<SpriteRenderer>().sprite = ivy_1;
        player.FaceTo(alive.transform.position);
        float cameraShakeTime = 1f;
        BasicCameraShake cameraShake = Camera.main.gameObject.GetComponent<BasicCameraShake>();
        cameraShake?.ShakeCameraVertically(cameraShakeTime, cameraShakeRange, cameraShakeSpeed, 1f, 2f);
        yield return new WaitForSeconds(cameraShakeTime);
        
        float duration = 2f;
        while(duration >= 0f){
            float y = Mathf.Lerp(ivy.transform.position.y, ivy.transform.position.y - 1f,ivyFallSpeed * Time.deltaTime);
            ivy.transform.position = new Vector3(ivy.transform.position.x, y, ivy.transform.position.z);
            Color color = ivy.GetComponent<SpriteRenderer>().color;
            color.a = Mathf.Clamp01(color.a - ivyFadeSpeed * Time.deltaTime);
            ivy.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForEndOfFrame();
            duration -= Time.deltaTime;
        }

        Animator doorAnim = alive.GetComponent<Animator>();
        doorAnim.Play("abandoned_door_open_0");
        yield return new WaitForEndOfFrame();
        duration = doorAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        cameraShake?.ShakeCameraVertically(duration, cameraShakeRange, cameraShakeSpeed);
        // TODO: generating particles
        yield return new WaitForSeconds(duration);

        InitOpenedDoor();

        player.stateMachine.SwitchState(player.idleState);
    }

    

}
