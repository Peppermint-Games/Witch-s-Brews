using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPlot : MonoBehaviour
{
    public Vector2 myPos;
    public PlantData heldData;
    public Plantdat saveData;
    public Image thisImg;
    public Sprite plantPlotSprite;
    public PlotState state;
    void Awake()
    {
        state = PlotState.Empty;
        thisImg.sprite = plantPlotSprite;
    }
    private void OnMouseDown()
    {
        if (saveData == null)
            return;
        if (!saveData.isUnlocked)
        {
            UnlockPlot();
            return;
        }
        {
            if (saveData.harvestCount > 0)
                GardenManager.I.HarvestPlant(this);
            state = PlotState.Harvested;
            UpdateVisuals(true);
        }
    }
    public void UnlockPlot()
    {
        if (saveData == null)
            return;
        if (saveData.isUnlocked)
            return;
        if (GameManager.I.save.gold >= GardenManager.I.unlockCost())
        {
            GameManager.I.save.gold -= GardenManager.I.unlockCost();
            saveData.isUnlocked = true;
            saveData.lastGrowthTick = GameManager.I.save.tickCount;
        }
        UpdateVisuals();
        GameManager.I.Save();
    }
    public void UpdateVisuals(bool harvested = false)
    {
        if (!harvested)
            if (saveData.harvestCount > 0)
                state = PlotState.Blooming;
            else
                state = PlotState.Growing;
        switch (state)
        {
            case PlotState.Harvested:
                thisImg.sprite = heldData.harvestSprite;
                break;
            case PlotState.Growing:
                thisImg.sprite = heldData.growSprite;
                break;
            case PlotState.Blooming:
                thisImg.sprite = heldData.bloomSprite;
                break;
        }
    }
}
