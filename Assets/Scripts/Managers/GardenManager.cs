using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public static GardenManager I;
    public List<PlantData> plantDatabase = new List<PlantData>();
    public gardenData thisData;
    public List<Sprite> plantIcons = new List<Sprite>();
    public List<PlantPlot> plots = new List<PlantPlot>();
    private void Awake()
    {
        I = this;
        BuildDatabase();
    }
    private void Start()
    {
        GardenSimulation.Update(GameManager.I.save);
        thisData = GameManager.I.save.data;
        BuildGarden();
    }
    void BuildDatabase()
    {
        plantDatabase.Clear();
        for (int i = 0; i < plantIcons.Count; i++)
        {
            PlantData newPlant = PlantDatabase.newPlant(i);
            newPlant.icon = plantIcons[i];
            plantDatabase.Add(newPlant);
        }
    }
    void BuildGarden()
    {
        foreach (var item in plots)
        {
            Plantdat data = thisData.plants.FirstOrDefault(x => x.myPos == item.myPos);
            if (data == null)
                continue;
            item.saveData = data;
            item.heldData = GetPlant(data.heldID);
            item.UpdateVisuals();
        }
    }
    public void HarvestPlant(PlantPlot plot)
    {
        if (plot == null)
            return;
        if (plot.saveData == null)
            return;
        if (plot.heldData == null)
            return;
        if (plot.saveData.harvestCount <= 0)
            return;
        plantInv inventoryItem = thisData.inventory.FirstOrDefault(x => x.plantID == plot.heldData.id);
        if(inventoryItem == null)
        {
            inventoryItem = new plantInv();
            inventoryItem.plantID = plot.heldData.id;
            inventoryItem.count = 0;
            thisData.inventory.Add(inventoryItem);
        }
        inventoryItem.count += plot.saveData.harvestCount;
        plot.saveData.harvestCount = 0;
        plot.UpdateVisuals();
        GameManager.I.Save();
    }
    public PlantData GetPlant(int id)
    {
        return plantDatabase.FirstOrDefault(item => item.id == id);
    }
    int getUnlockedCount()
    {
        int v = 0;
        foreach (var item in plots)
        {
            if (item.saveData.isUnlocked)
                v++;
        }
        return v;
    }
    public int unlockCost()
    {
        int count = getUnlockedCount();
        return count * count * count;
    }
}
public class PlantData
{
    public string plantName;
    [TextArea]
    public string description;
    public Sprite icon;
    public int growTime, yield, id, value;
    public vibe vibe;
    public timeScale scale = timeScale.day;
}