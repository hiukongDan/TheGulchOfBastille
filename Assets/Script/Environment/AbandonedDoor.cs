using UnityEngine;
using System.Collections;

public class AbandonedDoor : MonoBehaviour, IInteractable
{
    public Sprite abandonedDoorOpened;
    public Sprite abandonedDoorClosed;
    public GameObject areaTransition;
    public GameObject doorOpenTrigger;

    private GameObject alive;
    private GameObject leaf;
    private GameObject infoSign;

    private bool isInited = false;

    private Player player;

    void Awake(){
        alive = transform.Find("Alive").gameObject;
        leaf = transform.Find("Leaf").gameObject;
        infoSign = transform.Find("InfoSign").gameObject;

        alive.GetComponent<SpriteRenderer>().sprite = abandonedDoorClosed;
    }

    void OnEnable() {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player && player.miscData.isAbandonedDoorOpen && !isInited){
            InitOpenedDoor();
        }
    }

    void InitOpenedDoor(){
        leaf.SetActive(false);
        alive.GetComponent<SpriteRenderer>().sprite = abandonedDoorOpened;
        areaTransition.SetActive(true);
        doorOpenTrigger.SetActive(false);
        infoSign.SetActive(false);
        isInited = true;
    }

    public void OnInteraction(){
        infoSign?.GetComponentInChildren<Animator>().Play(InfoSignAnimHash.OUTRO);
        StartCoroutine(OpenAbandonedDoor());
        player.SetInteractable(null);
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
        player.stateMachine.SwitchState(player.cinemaState);

        float cameraShakeTime = 3f;
        float cameraShakeSpeed = 1f;
        float cameraShakeRange = 0.1f;

        Camera.main.gameObject.GetComponent<BasicCameraShake>()?.ShakeCameraVertically(cameraShakeTime, cameraShakeRange, cameraShakeSpeed, 1f, 2f);

        yield return new WaitForSeconds(cameraShakeTime);

        player.stateMachine.SwitchState(player.idleState);
    }

    

}
