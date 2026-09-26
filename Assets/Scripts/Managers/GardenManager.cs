using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenManager : MonoBehaviour
{
    public static GardenManager I;
    public List<PlantData> plantDatabase = new List<PlantData>();
    public gardenData thisData;
    public List<Sprite> harvestIcons, growingIcons, bloomIcons;
    public Sprite emptyPlot;
    public List<PlantPlot> plots = new List<PlantPlot>();
    private void Awake()
    {
        I = this;
        BuildDatabase();
    }
    private void OnDestroy() => I = null;
    private void Start()
    {
        GardenSimulation.UpdateThis(GameManager.I.save);
        thisData = GameManager.I.save.data.garden;
        BuildGarden();
    }
    void BuildDatabase()
    {
        plantDatabase.Clear();
        for (int i = 0; i < harvestIcons.Count; i++)
        {
            PlantData newPlant = PlantDatabase.newPlant(i);
            newPlant.harvestSprite = harvestIcons[i];
            newPlant.growSprite = growingIcons[i];
            newPlant.bloomSprite = bloomIcons[i];
            plantDatabase.Add(newPlant);
        }
    }
    void BuildGarden()
    {
        plots = FindObjectsOfType<PlantPlot>().OrderBy(x => x.transform.GetSiblingIndex()).ToList();
        const int plantCount = 40;
        const int plotsPerPlant = 5;
        for(int plantID = 0; plantID < plantCount; plantID++)
        {
            for(int row = 0; row < plotsPerPlant; row++)
            {
                int index = (plantID * plotsPerPlant) + row;
                if (index >= plots.Count)
                    continue;
                PlantPlot plot = plots[index];
                plot.myPos = new Vector2(plantID, row);
                Plantdat data = thisData.plants.FirstOrDefault(x => x.myPos == plot.myPos);
                if(data == null)
                {
                    data = new Plantdat
                    {
                        myPos = plot.myPos,
                        heldID = plantID,
                        harvestCount = 0,
                        isUnlocked = false,
                        lastGrowthTick = GameManager.I.save.tickCount
                    };
                    thisData.plants.Add(data);
                }
                plot.saveData = data;
                plot.heldData = GetPlant(data.heldID);
                plot.UpdateVisuals();
            }
        }
        GameManager.I.Save();
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
        if (inventoryItem == null)
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
    public Sprite harvestSprite, growSprite, bloomSprite;
}
