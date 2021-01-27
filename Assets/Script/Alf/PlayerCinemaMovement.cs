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

    private float playerLittleSunOffset = 8 * 1 / 32;
    
    public void LightLittleSun(LittleSunHandler littleSunHandler)
    {
        StartCoroutine(lightLittleSun(littleSunHandler.transform.position));
    }

    // set sun's position to fit player's
    IEnumerator lightLittleSun(Vector2 littleSunPos)
    {
        Vector2 playerTargetPos = new Vector2(littleSunPos.x + playerLittleSunOffset, littleSunPos.y);
        // the same hack here as the combat freeze of Goye
        player.stateMachine.SwitchState(player.converseState);

        return null;
    }


    #endregion
}
