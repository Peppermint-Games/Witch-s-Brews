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
    private void OnMouseDown()
    {
        if (saveData == null)
            return;
        if (!saveData.isUnlocked)
        {
            UnlockPlot();
            return;
        }
        if (saveData.harvestCount > 0)
            GardenManager.I.HarvestPlant(this);
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
    public void UpdateVisuals()
    {
        thisImg.sprite = heldData.icon;
        thisImg.color = saveData.isUnlocked ? Color.white : Color.black;
    }
}