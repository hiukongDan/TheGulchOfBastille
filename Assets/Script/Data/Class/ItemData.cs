using System.Collections;
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
        Count,
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
        Count
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
        Count,
    };
    public enum KeyItem{
        Dash_Stone,
        Double_Jump_Stone,
        Wall_Jump_Stone,
        Royal_Pass,
        Count,
    };


    public struct WeaponRuntimeData{
        public Weapon weapon;
        public int level;
        public WeaponRuntimeData(Weapon weapon, int level){
            this.weapon = weapon;
            this.level = level;
        }
        
    };

    public struct WearableRuntimeData{
        public Wearable wearable;
        public WearableRuntimeData(Wearable wearable){
            this.wearable = wearable;
        }
    };

    public struct ConsumableRuntimeData{
        public Consumable consumable;
        public int count;
        public ConsumableRuntimeData(Consumable consumable, int count){
            this.consumable = consumable;
            this.count = count;
        }
    };

    public struct KeyItemRuntimeData{
        public KeyItem keyItem;
        public KeyItemRuntimeData(KeyItem keyItem){
            this.keyItem = keyItem;
        }
    };

    

public static Dictionary<int , string> WeaponDescription = new Dictionary<int, string>(){
    {(int)Weapon.Iron_Sword, ""},
    {(int)Weapon.Claymore, ""},
    {(int)Weapon.Dragon_Slayer_Sword, ""},
    {(int)Weapon.Wood_Bow, ""},
    {(int)Weapon.Elf_Bow, ""},
    {(int)Weapon.Long_Bow, ""},
    {(int)Weapon.Apprentice_Stick, ""},
    {(int)Weapon.Master_Stick, ""},
    {(int)Weapon.Sunlight_Stick, ""}
};

public static Dictionary<int, string> WearableDescription = new Dictionary<int, string>(){

};

public static Dictionary<int, string> ConsumableDescription = new Dictionary<int, string>(){

};

public static Dictionary<int, string> KeyItemDescription = new Dictionary<int, string>(){

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
