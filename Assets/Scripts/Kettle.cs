using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettle : MonoBehaviour
{
    public Vector2 myPos;
    public Kettledat saveData;
    public Teabag heldData;
    public bool IsEmpty => saveData == null || saveData.heldID < 0;
    public bool IsBrewing => !IsEmpty && !saveData.ready;
    public bool IsReady => !IsEmpty && saveData.ready;
    public bool StartBrew(Teabag tea)
    {
        if (saveData == null)
            return false;
        if (!saveData.isUnlocked)
            return false;
        if (!IsEmpty)
            return false;
        if (tea == null)
            return false;
        saveData.heldID = tea.id;
        saveData.brewStartTick = GameManager.I.save.tickCount;
        saveData.ready = false;
        heldData = tea;
        GameManager.I.Save();
        UpdateVisuals();
        return true;
    }
    public Teabag CollectTea()
    {
        if (saveData == null)
            return null;
        if (!saveData.ready)
            return null;
        Teabag result = TeaShopManager.I.GetTeaByID(saveData.heldID);
        if (result == null)
            return null;
        TeaShopManager.I.AddTeaToInventory(result.id, 1);
        saveData.heldID = -1;
        saveData.brewStartTick = 0;
        saveData.ready = false;
        heldData = null;
        GameManager.I.Save();
        UpdateVisuals();
        return result;
    }
    public void UpdateVisuals()
    {

    }
}