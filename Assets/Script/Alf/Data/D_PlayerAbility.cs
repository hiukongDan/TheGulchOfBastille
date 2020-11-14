using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newAbilityData", menuName= "Data/Player/Ability Data")]
public class D_PlayerAbility : ScriptableObject
{
    public bool doubleJump = false;
    public bool wallJump = false;
    public bool dash = false;
}
