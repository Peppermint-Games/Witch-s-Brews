using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeaShopManager : MonoBehaviour
{
    public static TeaShopManager I;
    void Awake()
    {
        I = this;
        BuildDatabase();
    }
    private void OnDestroy() => I = null;
    public InputField teaName, teaDescription;
    public List<Teabag> teaDatabase = new List<Teabag>();
    public List<Sprite> vibeIcons = new List<Sprite>();
    public teaData thisData;
    public List<Kettle> kettles = new List<Kettle>();
    void BuildDatabase()
    {
        teaDatabase.Clear();
        for (int i = 0; i < 20; i++)
        {
            Teabag newTea = TeaDatabase.newTea(i);
            if (newTea == null)
                continue;
            newTea.icon = GetVibeIcon(newTea.teaVibe);
            teaDatabase.Add(newTea);
        }
    }
    private void Start()
    {
        TeaSimulation.UpdateThis(GameManager.I.save);
        thisData = GameManager.I.save.data.tea;
        BuildKettles();
    }
    void BuildKettles()
    {
        foreach (var item in kettles)
        {
            Kettledat data = thisData.kettles.FirstOrDefault(x => x.myPos == item.myPos);
            if (data == null)
                continue;
            item.saveData = data;
            if (data.heldID >= 0)
                item.heldData = GetTeaByID(data.heldID);
            else
                item.heldData = null;
            item.UpdateVisuals();
        }
    }
    public Teabag GetRecipe(List<PlantData> plants)
    {
        return GetRecipe(plants.Select(x => x.id).ToList());
    }
    public Teabag GetRecipe(List<int> plantIDs)
    {
        int[] sortedIDs = plantIDs.OrderBy(x => x).ToArray();
        return teaDatabase.FirstOrDefault(tea => tea.ingredients != null && tea.ingredients.Count == 4 && tea.ingredients.OrderBy(x => x).SequenceEqual(sortedIDs));
    }
    public Teabag CreateTea(List<PlantData> plants)
    {
        if (plants == null || plants.Count != 4)
            return null;
        if (plants.Any(x => x == null))
            return null;
        Teabag premade = GetRecipe(plants);
        if (premade != null)
            return premade;
        string key = TeaRecipeUtility.GetRecipeKey(plants);
        SavedTeaRecipe savedRecipe = FindSavedRecipe(key);
        if (savedRecipe != null)
            return ConvertSavedRecipe(savedRecipe);
        Teabag newTea = GenerateTea(plants);
        SaveNewRecipe(key, newTea);
        return newTea;
    }
    SavedTeaRecipe FindSavedRecipe(string key)
    {
        if (GameManager.I.save.data.tea.RBook.myRecipes == null)
            GameManager.I.save.data.tea.RBook.myRecipes = new List<SavedTeaRecipe>();
        return GameManager.I.save.data.tea.RBook.myRecipes.FirstOrDefault(x => x.recipeKey == key);
    }
    Teabag GenerateTea(List<PlantData> plants)
    {
        Teabag tea = new Teabag();
        tea.id = -1;
        tea.isPremade = false;
        tea.ingredients = plants.Select(x => x.id).OrderBy(x => x).ToList();
        tea.vibes = CalculateVibes(plants);
        tea.teaVibe = tea.vibes.OrderByDescending(x => x.value).First().type;
        tea.icon = GetVibeIcon(tea.teaVibe);
        tea.teaName = teaName.text;
        tea.growTime = Mathf.RoundToInt((float)plants.Average(x => x.growTime));
        tea.scale = timeScale.minute;
        tea.description = teaDescription.text;
        teaName.text = "";
        teaDescription.text = "";
        return tea;
    }
    public void AddTeaToInventory(int teaID, int amount)
    {
        TeaInv existing = thisData.inventory.FirstOrDefault(x => x.teaID == teaID);
        if (existing != null)
            existing.count += amount;
        else
        {
            thisData.inventory.Add(new TeaInv { teaID = teaID, count = amount });
        }
        GameManager.I.Save();
    }
    void SaveNewRecipe(string key, Teabag tea)
    {
        teaData data = GameManager.I.save.data.tea;
        tea.id = data.nextCustomTeaID;
        data.nextCustomTeaID++;
        SavedTeaRecipe save = new SavedTeaRecipe { id = tea.id, recipeKey = key, customName = tea.teaName, description = tea.description, plantIDs = new List<int>(tea.ingredients) };
        save.vibes = tea.vibes.Select(x => new VibeValue { type = x.type, value = x.value }).ToList();
        save.growTime = tea.growTime;
        data.RBook.myRecipes.Add(save);
        GameManager.I.Save();
    }
    public Kettle GetKettle(Vector2 position)
    {
        return kettles.FirstOrDefault(x => x.myPos == position);
    }
    Teabag ConvertSavedRecipe(SavedTeaRecipe saved)
    {
        if (saved == null)
            return null;
        Teabag tea = new Teabag
        {
            id = saved.id,
            teaName = saved.customName,
            description = saved.description,
            ingredients = new List<int>(saved.plantIDs),
            vibes = saved.vibes.Select(x => new VibeValue { type = x.type, value = x.value }).ToList(),
            growTime = saved.growTime,
            scale = timeScale.minute,
            isPremade = false
        };
        if (tea.vibes.Count > 0)
            tea.teaVibe = tea.vibes.OrderByDescending(x => x.value).First().type;
        return tea;
    }
    List<VibeValue> CalculateVibes(List<PlantData> plants)
    {
        List<VibeValue> results = new List<VibeValue>();
        foreach (PlantData plant in plants)
        {
            VibeValue existing = results.FirstOrDefault(x => x.type == plant.vibe);
            if (existing != null)
                existing.value += plant.value;
            else
                results.Add(new VibeValue { type = plant.vibe, value = plant.value });
        }
        return results;
    }
    public Teabag GetTeaByID(int id)
    {
        return TeaResolver.GetTeaByID(id, GameManager.I.save);
    }
    public void ServeTea(Customer customer, Teabag tea)
    {
        if (customer == null || tea == null)
            return;
        float score = TeaScoring.ScoreTea(tea, customer.order);
        int payment = TeaScoring.CalculatePayment(customer.order, score);
        GameManager.I.save.gold += payment;
        GameManager.I.Save();
        CustomerManager.I.GenerateCustomer();
    }
    public Sprite GetVibeIcon(vibe teaVibe)
    {
        int index = (int)teaVibe;
        if (index < 0 || index >= vibeIcons.Count)
            return null;
        return vibeIcons[index];
    }
    public int GetPlantCount(int plantID)
    {
        gardenData garden = GameManager.I.save.data.garden;
        plantInv item = garden.inventory.FirstOrDefault(x => x.plantID == plantID);
        return item != null ? item.count : 0;
    }
    public bool HasPlants(List<int> plantIDs)
    {
        if (plantIDs == null)
            return false;
        foreach (var item in plantIDs.GroupBy(x => x))
        {
            int owned = GetPlantCount(item.Key);
            int required = item.Count();
            if (owned < required)
                return false;
        }
        return true;
    }
    void RemovePlants(List<int> plantIDs)
    {
        gardenData garden = GameManager.I.save.data.garden;
        foreach (var group in plantIDs.GroupBy(x => x))
        {
            plantInv item = garden.inventory.FirstOrDefault(x => x.plantID == group.Key);
            if (item == null)
                continue;
            item.count -= group.Count();
            if (item.count <= 0) garden.inventory.Remove(item);
        }
    }
    public bool BrewTea(Kettle kettle, List<int> plantIDs)
    {
        if (kettle == null || kettle.saveData == null || !kettle.saveData.isUnlocked || !kettle.IsEmpty || plantIDs == null || plantIDs.Count != 4 || !HasPlants(plantIDs))
            return false;
        List<PlantData> plants = new List<PlantData>();
        foreach(var item in plantIDs)
        {
            PlantData plant = PlantDatabase.newPlant(item);
            if (plant == null)
                return false;
            plants.Add(plant);
        }
        Teabag tea = CreateTea(plants);
        if (tea == null)
            return false;
        bool started = kettle.StartBrew(tea);
        if (!started)
            return false;
        RemovePlants(plantIDs);
        GameManager.I.Save();
        return true;
    }
    public void TestBrew()
    {
        if (kettles.Count == 0)
            return;
        List<int> testIngredients = new List<int>{ 2, 3, 4, 5 };
        bool success = BrewTea(kettles[0], testIngredients);
        Debug.Log("success");
    }
}
public class Teabag
{
    public string teaName;
    [TextArea]
    public string description;
    public int id, growTime;
    public List<int> ingredients = new List<int>(4);
    public vibe teaVibe;
    public List<VibeValue> vibes = new List<VibeValue>();
    public timeScale scale = timeScale.minute;
    public Sprite icon;
    public bool isPremade = true;
    public int GetVibeValue(vibe type)
    {
        VibeValue result = vibes.FirstOrDefault(x => x.type == type);
        return result != null ? result.value : 0;
    }
}
public static class TeaRecipeUtility
{
    public static string GetRecipeKey(IEnumerable<int> plantIDs)
    {
        return string.Join("-", plantIDs.OrderBy(x => x));
    }
    public static string GetRecipeKey(IEnumerable<PlantData> plants)
    {
        return GetRecipeKey(plants.Select(x => x.id));
    }
}
public static class TeaResolver
{
    public static Teabag GetTeaByID(int id, saveData save)
    {
        if (id < 0)
            return null;
        if (id < 20)
            return TeaDatabase.newTea(id);
        if (save == null || save.data == null || save.data.tea == null || save.data.tea.RBook == null || save.data.tea.RBook.myRecipes == null)
            return null;
        SavedTeaRecipe saved = save.data.tea.RBook.myRecipes.FirstOrDefault(x => x.id == id);
        if (saved == null)
            return null;
        Teabag tea = new Teabag
        {
            id = saved.id,
            teaName = saved.customName,
            description = saved.description,
            ingredients = new List<int>(saved.plantIDs),
            growTime = saved.growTime,
            scale = timeScale.minute,
            isPremade = false
        };
        if (saved.vibes != null)
            tea.vibes = saved.vibes.Select(x => new VibeValue { type = x.type, value = x.value }).ToList();
        if (tea.vibes.Count > 0)
            tea.teaVibe = tea.vibes.OrderByDescending(x => x.value).First().type;
        return tea;
    }
}
public static class TeaScoring
{
    public static float ScoreTea(Teabag tea, CustomerOrder order)
    {
        if (tea == null || order == null) return 0f;
        if (order.wantsSpecificTea)
            return tea.id == order.requestedTeaID ? 100f : 0f;
        return ScoreVibes(tea, order);
    }
    public static int CalculatePayment(CustomerOrder order, float score)
    {
        float multiplier;
        if (score >= 90)
            multiplier = 2;
        else if (score >= 75)
            multiplier = 1.5f;
        else if (score >= 50)
            multiplier = 1;
        else if (score >= 25)
            multiplier = 0.5f;
        else
            multiplier = .25f;
        return Mathf.RoundToInt(order.basePay * multiplier);
    }
    static float ScoreVibes(Teabag tea, CustomerOrder order)
    {
        if (tea == null || order == null)
            return 0;
        if (order.requirements == null || order.requirements.Count == 0)
            return 0;
        float earned = 0, possible = 0;
        foreach (var item in order.requirements)
        {
            if (item == null)
                continue;
            if (item.weight <= 0)
                continue;
            int teaValue = tea.GetVibeValue(item.targetvibe);
            float requirementScore = Mathf.Clamp01(teaValue / 5f);
            earned += requirementScore * item.weight;
            possible += item.weight;
        }
        if (possible <= 0)
            return 0;
        return (earned / possible) * 100f;
    }
}