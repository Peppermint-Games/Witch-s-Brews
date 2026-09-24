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
    public int nextCustomTeaID = 1000, nextRegularID = 0;
    public List<TeaInv> inventory = new List<TeaInv>();
    public List<SeatedCustomerData> seatedCustomers = new List<SeatedCustomerData>();
    public List<RegularData> regulars = new List<RegularData>();
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
[Serializable]
public class Customer
{
    public string customerName;
    public CustomerOrder order;
    public CustomerState state;
    public bool isRegular;
    public int regularID = -1;
    public int chairID = -1;
}
[Serializable]
public class CustomerOrder
{
    public string requestText;
    public bool wantsSpecificTea;
    public List<VibeRequirement> requirements = new List<VibeRequirement>();
    public int basePay, requestedTeaID = -1;
}
[Serializable]
public class VibeRequirement
{
    public vibe targetvibe;
    public int weight;
}
[Serializable]
public class OrderKeyword
{
    public string text;
    public vibe targetVibe;
    public int weight = 1;
}
[Serializable]
public class VibeValue
{
    public vibe type;
    public int value;
}
[Serializable]
public class VibeModifier
{
    public vibe type;
    public int weight;
}
[Serializable]
public class OrderKeyWord
{
    [TextArea]
    public string text;
    public List<VibeModifier> modifiers = new List<VibeModifier>();
}
[Serializable]
public class SeatedCustomerData
{
    public int chairID;
    public string customerName;
    public bool wantsSpecificTea;
    public int requestedTeaID = -1;
    public List<VibeRequirement> requirements = new List<VibeRequirement>();
    public int basePay;
    public bool isRegular;
    public int regularID = -1;
}
[Serializable]
public class RegularData
{
    public int id;
    public string customerName;
    public List<int> preferredTeaIDs = new List<int>();
    public List<vibe> preferredVibes = new List<vibe>();
    public int visits, successfulVisits;
    public float tipBonus = 0.25f;
}