using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinemaMovement : MonoBehaviour
{
    private Player player;
    
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    #region CINEMA FUNCTIONS

    public void LightLittleSun(LittleSunHandler littleSunHandler)
    {
        StartCoroutine(lightLittleSun(littleSunHandler));
    }

    // set sun's position to fit player's
    IEnumerator lightLittleSun(LittleSunHandler littleSunHandler)
    {
        float playerLittleSunOffset = 0.25f;
        Vector2 littleSunPos = littleSunHandler.transform.position;
        Vector2 targetPos = new Vector2(littleSunPos.x + playerLittleSunOffset, littleSunPos.y);
        // the same hack here as the combat freeze of Goye
        player.stateMachine.SwitchState(player.cinemaState);
        // face towards the targetpos
        player.FaceTo(targetPos);
        // walk towards it
        player.Anim.Play(AlfAnimationHash.RUN_0);
        player.SetVelocity(new Vector2(player.facingDirection * player.playerData.WS_walkSpeed / 5, 0));
        // yield return new WaitUntil(player at target pos);
        yield return new WaitWhile(() => Mathf.Abs(player.transform.position.x - targetPos.x) > 0.001f);
        player.SetVelocity(Vector2.zero);
        // face towards little sun
        player.FaceTo(littleSunPos);
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
        
    }


    #endregion
}
