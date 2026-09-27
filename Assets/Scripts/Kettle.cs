using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kettle : MonoBehaviour
{
    public Vector2 myPos;
    public Kettledat saveData;
    public Teabag heldData;
    public Sprite empty, brewing, brewed;
    public Image thisImg;
    private void Awake()
    {
        thisImg = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(() => Interact());
    }
    public void Interact()
    {
        if (IsEmpty())
        {
            RecipeBookUIManager.I.OpenForKettle(this);
            return;
        }
        if (IsReady())
        {
            CollectTea();
            RecipeBookUIManager.I.OpenForKettle(this);
            return;
        }
    }
    public bool IsEmpty()
    {
        return saveData != null &&
               saveData.heldID < 0;
    }
    public bool IsBrewing()
    {
        return saveData != null &&
               saveData.heldID >= 0 &&
               !saveData.ready;
    }
    public bool IsReady()
    {
        return saveData != null &&
               saveData.heldID >= 0 &&
               saveData.ready;
    }
    public bool StartBrew(Teabag tea)
    {
        if (saveData == null)
            return false;

        if (!IsEmpty())
            return false;

        if (tea == null)
            return false;

        saveData.heldID = tea.id;
        saveData.brewStartTick =
            GameManager.I.save.tickCount;

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
        TeaShopManager.I.AddBrewedTea(result.id, 1);
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
        if (thisImg == null)
            thisImg = GetComponent<Image>();
        if (thisImg == null)
            return;
        if (IsReady())
            thisImg.sprite = brewed;
        else if (IsBrewing())
            thisImg.sprite = brewing;
        else
            thisImg.sprite = empty;
    }
}