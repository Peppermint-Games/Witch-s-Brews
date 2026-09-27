using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager I;
    public List<Teabag> knownRecipes = new List<Teabag>();
    public Transform ingredientGrid;
    public GameObject ingredientPrefab;
    public List<Sprite> plantIcons = new List<Sprite>();
    public List<PlantData> mixer = new List<PlantData>();
    public Image[] mixerImages = new Image[4];
    public GameObject craftingUI;
    public Image outputVibeIcon;
    public Text outputTeaName;
    public Button confirmCraftButton;
    Teabag previewTea;
    bool previewIsKnown;
    private void Awake()
    {
        I = this;
    }
    public void ToggleCraftingUI() => craftingUI.SetActive(!craftingUI.activeInHierarchy);
    private void Start()
    {
        BuildRecipeBook();
        BuildIngredientButtons();
    }
    public void BuildIngredientButtons()
    {
        if (ingredientGrid == null)
        {
            Debug.LogError(
                "CraftingManager: ingredientGrid is null."
            );

            return;
        }

        if (ingredientPrefab == null)
        {
            Debug.LogError(
                "CraftingManager: ingredientPrefab is null."
            );

            return;
        }

        // Clear old buttons.
        for (int i = ingredientGrid.childCount - 1;
             i >= 0;
             i--)
        {
            Destroy(
                ingredientGrid.GetChild(i).gameObject
            );
        }

        if (GameManager.I == null ||
            GameManager.I.save == null ||
            GameManager.I.save.data == null ||
            GameManager.I.save.data.garden == null ||
            GameManager.I.save.data.garden.inventory == null)
        {
            Debug.LogError(
                "CraftingManager: Garden inventory is unavailable."
            );

            return;
        }

        List<plantInv> inventory =
            GameManager.I.save.data.garden.inventory;

        foreach (plantInv inventoryItem in inventory)
        {
            if (inventoryItem == null)
                continue;

            if (inventoryItem.count <= 0)
                continue;

            PlantData plant =
                PlantDatabase.newPlant(
                    inventoryItem.plantID
                );

            if (plant == null)
            {
                Debug.LogWarning(
                    "Could not find PlantData ID " +
                    inventoryItem.plantID
                );

                continue;
            }

            Sprite sprite = GetPlantIcon(
                inventoryItem.plantID
            );

            GameObject obj =
                Instantiate(
                    ingredientPrefab,
                    ingredientGrid
                );

            CraftingIngredientButton button =
                obj.GetComponent<
                    CraftingIngredientButton>();

            if (button == null)
            {
                Debug.LogError(
                    "Ingredient prefab has no " +
                    "CraftingIngredientButton."
                );

                Destroy(obj);
                continue;
            }

            button.Setup(
                plant,
                sprite,
                inventoryItem.count
            );
        }
    }
    Sprite GetPlantIcon(int plantID)
    {
        if (plantID < 0)
            return null;

        if (plantID >= plantIcons.Count)
        {
            Debug.LogWarning(
                "No plant icon assigned for plant ID " +
                plantID
            );

            return null;
        }

        return plantIcons[plantID];
    }
    public bool AddToMixer(PlantData plant)
    {
        if (plant == null)
            return false;

        if (mixer.Count >= 4)
            return false;

        // Must actually own this ingredient.
        if (TeaShopManager.I.GetPlantCount(
            plant.id) <=
            GetMixerCount(plant.id))
        {
            return false;
        }

        mixer.Add(plant);

        RefreshMixerUI();

        return true;
    }
    void RefreshMixerUI()
    {
        for (int i = 0;
             i < mixerImages.Length;
             i++)
        {
            if (mixerImages[i] == null)
                continue;

            if (i >= mixer.Count)
            {
                mixerImages[i].sprite = null;
                mixerImages[i].enabled = false;
                continue;
            }

            PlantData plant = mixer[i];

            if (plant == null)
            {
                mixerImages[i].sprite = null;
                mixerImages[i].enabled = false;
                continue;
            }

            if (plant.id >= 0 &&
                plant.id < plantIcons.Count)
            {
                mixerImages[i].sprite =
                    plantIcons[plant.id];

                mixerImages[i].enabled = true;
            }
        }
        RefreshOutput();
    }
    void RefreshOutput()
    {
        previewTea = null;
        previewIsKnown = false;

        // Not enough ingredients yet.
        if (mixer.Count != 4)
        {
            if (outputTeaName != null)
                outputTeaName.text = "";

            if (outputVibeIcon != null)
            {
                outputVibeIcon.sprite = null;
                outputVibeIcon.enabled = false;
            }

            if (confirmCraftButton != null)
                confirmCraftButton.interactable = false;

            return;
        }

        // Find out whether we've already discovered this exact recipe.
        previewTea = FindKnownRecipe(mixer);

        if (previewTea != null)
        {
            previewIsKnown = true;

            if (outputTeaName != null)
                outputTeaName.text = previewTea.teaName;

            if (outputVibeIcon != null)
            {
                outputVibeIcon.sprite =
                    TeaShopManager.I.GetVibeIcon(
                        GetPrimaryVibe(previewTea)
                    );

                outputVibeIcon.enabled =
                    outputVibeIcon.sprite != null;
            }
        }
        else
        {
            // Unknown recipe.
            if (outputTeaName != null)
                outputTeaName.text = "?????";

            vibe predictedVibe =
                CalculateMixerPrimaryVibe();

            if (outputVibeIcon != null)
            {
                outputVibeIcon.sprite =
                    TeaShopManager.I.GetVibeIcon(
                        predictedVibe
                    );

                outputVibeIcon.enabled =
                    outputVibeIcon.sprite != null;
            }
        }

        if (confirmCraftButton != null)
            confirmCraftButton.interactable = true;
    }
    Teabag FindKnownRecipe(
   List<PlantData> plants)
    {
        if (plants == null ||
            plants.Count != 4)
            return null;

        List<int> selectedIDs =
            plants
            .Select(x => x.id)
            .OrderBy(x => x)
            .ToList();

        foreach (Teabag tea in knownRecipes)
        {
            if (tea == null ||
                tea.ingredients == null ||
                tea.ingredients.Count != 4)
                continue;

            List<int> recipeIDs =
                tea.ingredients
                .OrderBy(x => x)
                .ToList();

            if (selectedIDs.SequenceEqual(recipeIDs))
                return tea;
        }

        return null;
    }
    public void ConfirmCraft()
    {
        if (mixer.Count != 4)
            return;

        // Make sure we still own all four ingredients.
        List<int> ingredientIDs =
            mixer
            .Select(x => x.id)
            .ToList();

        if (!TeaShopManager.I.HasPlants(
            ingredientIDs))
            return;

        Teabag tea = FindKnownRecipe(mixer);

        // This is a genuinely new combination.
        if (tea == null)
        {
            tea = TeaShopManager.I.CreateTea(mixer);

            if (tea == null)
                return;

            // The new custom recipe now belongs
            // in our known recipe list.
            BuildRecipeBook();
        }

        // Only consume AFTER we've successfully
        // resolved/created the tea.
        TeaShopManager.I.RemovePlants(
            ingredientIDs
        );

        TeaShopManager.I.AddTeaToInventory(
            tea.id,
            1
        );

        GameManager.I.Save();

        ClearMixer();
    }
    vibe CalculateMixerPrimaryVibe()
    {
        Dictionary<vibe, int> values =
            new Dictionary<vibe, int>();

        foreach (PlantData plant in mixer)
        {
            if (plant == null)
                continue;

            if (!values.ContainsKey(plant.vibe))
                values.Add(plant.vibe, 0);

            values[plant.vibe] += plant.value;
        }

        if (values.Count == 0)
            return vibe.Chill;

        return values
            .OrderByDescending(x => x.Value)
            .First()
            .Key;
    }
    public void RemoveFromMixer(int index)
    {
        if (index < 0 ||
            index >= mixer.Count)
            return;

        mixer.RemoveAt(index);

        RefreshMixerUI();
    }

    public void ClearMixer()
    {
        mixer.Clear();

        RefreshMixerUI();
    }

    int GetMixerCount(int plantID)
    {
        return mixer.Count(
            x => x != null &&
                 x.id == plantID
        );
    }
    public void BuildRecipeBook()
    {
        knownRecipes.Clear();
        if (TeaShopManager.I == null)
            return;
        foreach (var item in TeaShopManager.I.teaDatabase)
        {
            if (item != null)
                knownRecipes.Add(item);
        }
        if (TeaShopManager.I.thisData != null && TeaShopManager.I.thisData.RBook != null)
        {
            foreach (var item in TeaShopManager.I.thisData.RBook.myRecipes)
            {
                Teabag tea = TeaShopManager.I.ConvertSavedRecipe(item);
                if (tea == null)
                    continue;
                if (knownRecipes.Any(x => x.id == tea.id))
                    continue;
                knownRecipes.Add(tea);
            }
        }
    }
    public Teabag GetRecipe(int teaID)
    {
        return knownRecipes.FirstOrDefault(x => x.id == teaID);
    }
    public Teabag GetRecipeAt(int index)
    {
        if (index < 0 || index >= knownRecipes.Count)
            return null;
        return knownRecipes[index];
    }
    public int RecipeCount()
    {
        return knownRecipes.Count;
    }
    public bool CanCraft(Teabag tea)
    {
        if (tea == null || tea.ingredients == null || tea.ingredients.Count != 4)
            return false;
        List<int> ingredientIDs = GetIngredientIDs(tea);
        return TeaShopManager.I.HasPlants(ingredientIDs);
    }
    public bool Craft(Teabag tea)
    {
        if (!CanCraft(tea))
            return false;
        List<int> ingredientIDs = GetIngredientIDs(tea);
        TeaShopManager.I.RemovePlants(ingredientIDs);
        TeaShopManager.I.AddTeaToInventory(tea.id, 1);
        GameManager.I.Save();
        return true;
    }
    public bool Craft(int teaID)
    {
        Teabag tea = GetRecipe(teaID);
        return Craft(tea);
    }
    public int GetOwnedTeabags(Teabag tea)
    {
        if (tea == null || TeaShopManager.I.thisData == null)
            return 0;
        TeaInv inventory = TeaShopManager.I.thisData.inventory.FirstOrDefault(x => x.teaID == tea.id);
        if (inventory == null)
            return 0;
        return inventory.count;
    }

    public int GetOwnedPlantCount(int plantID)
    {
        return TeaShopManager.I.GetPlantCount(plantID);
    }

    public int GetRequiredPlantCount(Teabag tea, int plantID)
    {
        if (tea == null || tea.ingredients == null)
            return 0;
        return tea.ingredients.Count(x => x == plantID);
    }
    public List<int> GetIngredientIDs(Teabag tea)
    {
        if (tea == null || tea.ingredients == null)
            return new List<int>();
        return new List<int>(tea.ingredients);
    }
    public vibe GetPrimaryVibe(Teabag tea)
    {
        if (tea == null || tea.vibes == null || tea.vibes.Count == 0)
            return vibe.Chill;
        VibeValue highest = tea.vibes[0];

        for (int i = 1; i < tea.vibes.Count; i++)
        {
            if (tea.vibes[i].value > highest.value)
                highest = tea.vibes[i];
        }
        return highest.type;
    }
}