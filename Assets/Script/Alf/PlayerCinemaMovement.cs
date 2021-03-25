using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinemaMovement : MonoBehaviour
{
    public Player player;
    private GameManager gm;

    public float gameBeginWaitForSec = 2f;

    void Awake(){
        gm = GetComponent<GameManager>();
    }

    void OnEnable(){
        AreaTransmissionHandler.Instance.performAreaTransmissionHandler += TransitToScene;
    }

    void OnDisable() {
        AreaTransmissionHandler.Instance.performAreaTransmissionHandler -= TransitToScene;
    }

    #region CINEMA FUNCTIONS

    public void LightLittleSun(LittleSunHandler littleSunHandler) => StartCoroutine(lightLittleSun(littleSunHandler));
    public void TransitToScene(SubAreaHandler subAreaHandler) => StartCoroutine(transitToScene(subAreaHandler));

    IEnumerator lightLittleSun(LittleSunHandler littleSunHandler)
    {
        float playerLittleSunOffset = 0.25f;
        Vector2 littleSunPos = littleSunHandler.transform.position;
        Vector2 targetPos = new Vector2(littleSunPos.x + playerLittleSunOffset, littleSunPos.y);
        // the same hack here as the combat freeze of Goye
        player.stateMachine.SwitchState(player.cinemaState);

        littleSunHandler.InfoSignAnim.Play(InfoSignAnimHash.OUTRO);
        // yield return new WaitUntil(player at target pos);
        if(Mathf.Abs(player.transform.position.x - targetPos.x) >= 1/32f)
        {
            // face towards the targetpos
            player.FaceTo(targetPos);
            // walk towards it
            player.Anim.Play(AlfAnimationHash.RUN_0);
            player.SetVelocity(new Vector2(player.facingDirection * player.playerData.WS_walkSpeed / 5, 0));

            yield return new WaitUntil(() => Mathf.Abs(player.transform.position.x - targetPos.x) <= 1/32f);
        }

        // face towards little sun
        player.FaceTo(littleSunPos);
        player.SetVelocity(Vector2.zero);
        // player.Anim.Play()
        player.Anim.Play(AlfAnimationHash.LIGHT_LITTLE_SUN_0);
        
        yield return new WaitUntil(() => player.lightingLittleSunToken == true);
        littleSunHandler.BloodAnim.Play("blood");

        yield return new WaitUntil(() => littleSunHandler.BloodAnim == null);
        littleSunHandler.LittleSunAnim.Play(LittleSunAnimHash.GENERATE_0);
        // yield return new WaitUntil(finish lighting sun)
        yield return new WaitUntil(() => littleSunHandler.LittleSunAnim.GetCurrentAnimatorStateInfo(0).shortNameHash == LittleSunAnimHash.IDLE_0);

        // player.stateMachine.switchstate(player.idleState);
        player.stateMachine.SwitchState(player.idleState);

        littleSunHandler.InfoSignAnim.Play(InfoSignAnimHash.INTRO);

    }
    IEnumerator transitToScene(SubAreaHandler subAreaHandler)
    {
        Door door = subAreaHandler.gameObject.GetComponentInChildren<Door>();
        player.stateMachine.SwitchState(player.cinemaState);
        if(door != null){
            // Open Door
            Animator doorAnim = door.GetComponent<Animator>();
            door.Open();
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(doorAnim.GetCurrentAnimatorStateInfo(0).length);
        }
        // Play Transition UI Effect
        // float time = gm.uiHandler.uiEffectHandler.OnPlayUIEffect(subAreaHandler.uIEffect, UIEffectAnimationClip.start);
        yield return new WaitForSeconds(gm.uiHandler.uiEffectHandler.OnPlayUIEffect(subAreaHandler.uIEffect, UIEffectAnimationClip.start));
        // TODO:
        /* startEffect */
        /* loading/enable scene prefab */
        /* endEffect */
        
        // Enable and Disable scenes
        // move player to new position
        yield return new WaitForSeconds(UIEffectData.CROSS_FADE_DELAY/2);

        if(subAreaHandler.targetSceneInitPos != null){
            player.transform.position = subAreaHandler.targetSceneInitPos.position;
        }
        door?.Close();
        gm.LoadSceneCode(subAreaHandler.transitionSceneCode);
        Camera.main.GetComponent<BasicFollower>().ClampCamera(player.transform.position);

        player.ResetGrounded();

        yield return new WaitForSeconds(UIEffectData.CROSS_FADE_DELAY/2);

        //yield return new WaitForSeconds(UIEffectData.CROSS_FADE_DELAY);
        
        yield return new WaitForSeconds(gm.uiHandler.uiEffectHandler.OnPlayUIEffect(subAreaHandler.uIEffect, UIEffectAnimationClip.end));

        // set sub area handler to null

        player.SetSubAreaHandler(null);

        player.stateMachine.SwitchState(player.idleState);
    }
    #endregion
}
