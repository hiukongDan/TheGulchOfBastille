using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleSunHandler : MonoBehaviour
{
    public LittleSunData.LittleSunID littleSunID;

    public void OnLittleSunInteraction()
    {
        Debug.Log("Little sun lit");
        if (!LittleSunData.Instance.IsLit(littleSunID))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playerCinemaMovement.LightLittleSun(this);
        }
        else
        {
            // Do Other interaction like leveling up or something
        }
    }
}
