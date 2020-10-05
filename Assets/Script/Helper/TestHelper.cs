using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class TestHelper : MonoBehaviour
{
    public SlowMutant1 enemy;
    public Player player;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Walk Speed: " + player.playerData.WS_walkSpeed;
    }
}
