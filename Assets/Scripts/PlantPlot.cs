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
        if (thisImg == null)
            thisImg = GetComponent<Image>();
    }
    private void Start()
    {
        thisImg.sprite = plantPlotSprite;
    }
    public void OnClicky() => UIManager.I.CallContextUI(this);
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
    public void HarvestPlot()
    {
        if (saveData.harvestCount <= 0)
            return;
        GardenManager.I.HarvestPlant(this);
        state = PlotState.Harvested;
        UpdateVisuals(true);
    }
    public void UpdateVisuals(bool harvested = false)
    {
        if (!saveData.isUnlocked)
        {
            thisImg.sprite = GardenManager.I.emptyPlot;
            return;
        }
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
