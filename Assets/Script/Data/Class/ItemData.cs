﻿using System.Collections;
using System.Collections.Generic;

public class ItemData
{
    public enum Weapon{
        Iron_Sword,
        Claymore,
        Dragon_Slayer_Sword,
        Wood_Bow,
        Elf_Bow,
        Long_Bow,
        Apprentice_Stick,
        Master_Stick,
        Sunlight_Stick,
    };

    public enum Wearable{
        Moonstone_Ring,
        Fools_Gold_Pendant,
        Sunlight_Protection_Stone,
        Amber_Ring,
        Drawf_Ring,
        Sunstone_Ring,
        Coldblue_Ring,
        Magic_Pendant,
        Belials_Magic_Compass,
        Goye_Ring,
    };

    public enum Consumable{
        Uilos_Candy,
        Uilos_Potion,
        Uilos_Cake,
        Holy_Sun_Water,
        Uilos_Pedal,
        Uilos_Flower,
        Uilos_Stick,
        Uilos_Bunch,
        Neon_Potion,
    };

    public enum KeyItem{
        Dash_Stone,
        Double_Jump_Stone,
        Wall_Jump_Stone,
        Royal_Pass,
    };

public static Dictionary<int, WeaponData> weaponData = new Dictionary<int, WeaponData>(){
    {(int)Weapon.Iron_Sword, new WeaponData(new int[3]{5,7,9}, true, false, true)},
    {(int)Weapon.Claymore, new WeaponData(new int[3]{10,12,14}, true, false, true)},
    {(int)Weapon.Dragon_Slayer_Sword, new WeaponData(new int[3]{15,17,19}, true, false, true)},
    {(int)Weapon.Wood_Bow, new WeaponData(new int[3]{8,10,12}, false, true, false)},
    {(int)Weapon.Elf_Bow, new WeaponData(new int[3]{10,12,14}, false, true, false)},
    {(int)Weapon.Long_Bow, new WeaponData(new int[3]{15,17,19}, true, false, true)},
    {(int)Weapon.Apprentice_Stick, new WeaponData(new int[3]{5,7,9}, false, true, false)},
    {(int)Weapon.Master_Stick, new WeaponData(new int[3]{8,10,12}, false, true, false)},
    {(int)Weapon.Sunlight_Stick, new WeaponData(new int[3]{15,17,19}, false ,true, false)},
};


    public struct WeaponData{
        int[] Attack;
        bool IsMelee;
        bool IsRange;
        bool CanParry;
        public WeaponData(int[] attack, bool isMelee, bool isRange, bool canParry){
            this.Attack = attack;
            this.IsMelee = isMelee;
            this.IsRange = isRange;
            this.CanParry = canParry;
        }
    };

    public static PlayerLevelUpData[] playerLevelUpData = {
        new PlayerLevelUpData(1,60,5,20),
        new PlayerLevelUpData(2,70,7,25),
        new PlayerLevelUpData(3,80,9,30),
        new PlayerLevelUpData(4,90,11,35),
        new PlayerLevelUpData(5,100,13,40),
        new PlayerLevelUpData(6,105, 15, 43),
        new PlayerLevelUpData(7, 110, 17, 46),
        new PlayerLevelUpData(8,115, 18, 48),
        new PlayerLevelUpData(9, 120, 19, 49),
        new PlayerLevelUpData(10, 125, 20, 50),
    };

    public struct PlayerLevelUpData{
        public int level;
        /// <Summary>
        /// Hit point
        /// </Summary>
        public int HP;
        /// <Summary>
        /// Attack point
        /// </Summary>
        int AP;
        /// <Summary>
        /// Decay point
        /// </Summary>
        int DP;

        public PlayerLevelUpData(int level, int hp, int ap, int dp){
            this.level = level;
            this.HP = hp;
            this.AP = ap;
            this.DP = dp;
        }
    };
}
