using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingIngredientButton : MonoBehaviour
{
    public PlantData plant;
    public Image plantImage;
    public Text countText;
    public Button button;
    int ownedCount;
    private void Awake()
    {
        plantImage = GetComponent<Image>();
        if (button == null)
            button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Interact);
    }
    public void Setup(PlantData newPlant, Sprite sprite, int count)
    {
        plant = newPlant;
        ownedCount = count;
        if (plantImage != null)
            plantImage.sprite = sprite;
        countText.text = "x" + count;
    }
    public int GetOwnedCount()
    {
        return ownedCount;
    }
    public void Interact()
    {
        if (plant == null)
            return;
        UIManager.I.CallContextUI(this);
    }
}