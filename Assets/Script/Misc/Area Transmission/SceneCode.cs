﻿using System.Collections.Generic;
public enum SceneCode
{
    Gulch_Main,
    Gulch_AlfHouse,
    Gulch_Graveyard,
    Gulch_Church_Corridor,
    Gulch_Church_Altar,
    Gulch_SunTower,
    Gulch_Tunnel,
    Gulch_Goye,
    Abandoned_Door,
    Mine_Entrance,
    Lower_Mine,
    Dwarf_Ruins,
    Mine_Center,
    Elf_Ruins,
    Elf_City,
    Old_Scaffold,
    Count,
};

public static class SceneCodeDisplayName{
    public static Dictionary<int, string> names = new Dictionary<int, string>(){
        {(int)SceneCode.Gulch_Main, "the Gulch"},
        {(int)SceneCode.Gulch_AlfHouse, "Alf house"},
        {(int)SceneCode.Gulch_Graveyard, "Graveyard"},
        {(int)SceneCode.Gulch_Church_Corridor, "Church Corridor"},
        {(int)SceneCode.Gulch_Church_Altar, "Church Altar"},
        {(int)SceneCode.Gulch_SunTower, "Sun Tower"},
        {(int)SceneCode.Gulch_Tunnel, "mine Tunnel"},
        {(int)SceneCode.Gulch_Goye, "prey of goye"},
        {(int)SceneCode.Abandoned_Door, "Abandoned Door"},
        {(int)SceneCode.Mine_Entrance, "Mine Entrance"},
        {(int)SceneCode.Lower_Mine, "Lower Mine"},
        {(int)SceneCode.Mine_Center, "Mine Center"},
        {(int)SceneCode.Dwarf_Ruins, "Dwarf Ruins"},
        {(int)SceneCode.Elf_Ruins, "Elf Ruins"},
        {(int)SceneCode.Elf_City, "Elf City"},
        {(int)SceneCode.Old_Scaffold, "Old Scaffold"},
    };
};

