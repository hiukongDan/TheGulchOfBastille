using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System;

[CreateAssetMenu(fileName="newAbilityData", menuName= "Data/Player/Ability Data")]
public class D_PlayerAbility : ScriptableObject
{
    [Serializable]
    public struct PlayerAbility
    {
        public bool doubleJump;
        public bool wallJump;
        public bool dash;

        public PlayerAbility(bool doubleJump, bool wallJump, bool dash)
        {
            this.doubleJump = doubleJump;
            this.wallJump = wallJump;
            this.dash = dash;
        }
    }

    public bool doubleJump;
    public bool wallJump;
    public bool dash;

    public PlayerAbility GetPlayerAbility()
    {
        return new PlayerAbility(doubleJump, wallJump, dash);
    }

    public void SetPlayerAbility(PlayerAbility ability)
    {
        this.doubleJump = ability.doubleJump;
        this.wallJump = ability.wallJump;
        this.dash = ability.dash;
    }
}
