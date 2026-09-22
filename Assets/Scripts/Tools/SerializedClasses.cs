using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class saveData
{
    public gardenData data;
    public int gold, currentDay;
    public long tickCount;
}
[Serializable]
public class gardenData
{
    public List<Plantdat> plants;
    public List<plantInv> inventory;
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
public class plantInv
{
    public int plantID, count;
}