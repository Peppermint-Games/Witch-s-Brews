using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class saveData
{
    public gameData data;
    public int gold, currentDay;
    public long tickCount;
}
[Serializable]
public class gameData
{
    public gardenData garden = new gardenData();
    public teaData tea = new teaData();
}
[Serializable]
public class gardenData
{
    public List<Plantdat> plants;
    public List<plantInv> inventory;
}
[Serializable]
public class teaData
{
    public recipeBook RBook = new recipeBook();
    public List<Kettledat> kettles;
    public int nextCustomTeaID = 1000;
    public List<TeaInv> inventory = new List<TeaInv>();
}
[Serializable]
public class Plantdat
{
    public int heldID, harvestCount;
    public Vector2 myPos;
    public bool isUnlocked;
    public long lastGrowthTick;
}
[Serializable]
public class Kettledat
{
    public int heldID;
    public Vector2 myPos;
    public bool isUnlocked;
    public long brewStartTick;
    public bool ready;
}
[Serializable]
public class plantInv
{
    public int plantID, count;
}
[Serializable]
public class recipeBook
{
    public List<SavedTeaRecipe> myRecipes;
}
[Serializable]
public class SavedTeaRecipe
{
    public string recipeKey, customName, description;
    public List<int> plantIDs = new List<int>();
    public int id;
    public vibe teaVibe;
    public int growTime;
}
[Serializable]
public class TeaInv
{
    public int teaID, count;
}