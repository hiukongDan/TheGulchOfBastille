using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinemaMovement : MonoBehaviour
{
    private Player player;
    private GameManager gm;
    
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    #region CINEMA FUNCTIONS

    public void LightLittleSun(LittleSunHandler littleSunHandler) => StartCoroutine(lightLittleSun(littleSunHandler));
    public void TransitToScene(UIEffect uiEffect, SceneCode sceneCode) => StartCoroutine(transitToScene(uiEffect, sceneCode));

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

    IEnumerator transitToScene(UIEffect useUIEffect, SceneCode sceneCode)
    {
        // TODO:
        /* startEffect */
        /* loading/enable scene prefab */
        /* endEffect */
        yield return null;
    }
    #endregion
}
