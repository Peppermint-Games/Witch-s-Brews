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
    public InputField teaName, teaDescription;
    public List<Teabag> teaDatabase = new List<Teabag>();
    public List<Sprite> premadeTeaIcon = new List<Sprite>();
    public teaData thisData;
    public List<Kettle> kettles = new List<Kettle>();
    void BuildDatabase()
    {
        teaDatabase.Clear();
        for (int i = 0; i < premadeTeaIcon.Count; i++)
        {
            Teabag newTea = TeaDatabase.newTea(i);
            if (newTea == null)
                continue;
            newTea.icon = premadeTeaIcon[i];
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
        tea.teaVibe = CalculateVibe(plants);
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
        save.teaVibe = tea.teaVibe;
        save.growTime = tea.growTime;
        GameManager.I.save.data.tea.RBook.myRecipes.Add(save);
        GameManager.I.Save();
    }
    public Kettle GetKettle(Vector2 position)
    {
        return kettles.FirstOrDefault(x => x.myPos == position);
    }
    Teabag ConvertSavedRecipe(SavedTeaRecipe saved)
    {
        return new Teabag { id = saved.id, teaName = saved.customName, ingredients = new List<int>(saved.plantIDs), teaVibe = saved.teaVibe, growTime = saved.growTime, scale = timeScale.minute, isPremade = false };
    }
    vibe CalculateVibe(List<PlantData> plants)
    {
        Dictionary<vibe, int> scores = new Dictionary<vibe, int>();
        foreach (PlantData plant in plants)
        {
            if (!scores.ContainsKey(plant.vibe))
                scores.Add(plant.vibe, 0);
            scores[plant.vibe] += plant.value;
        }
        return scores.OrderByDescending(x => x.Value).First().Key;
    }
    public Teabag GetTeaByID(int id)
    {
        if (id < 0)
            return null;
        Teabag premade = teaDatabase.FirstOrDefault(x => x.id == id);
        if (premade != null)
            return premade;
        SavedTeaRecipe saved = GameManager.I.save.data.tea.RBook.myRecipes.FirstOrDefault(x => x.id == id);
        if (saved != null)
            return ConvertSavedRecipe(saved);
        return null;
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
    public timeScale scale = timeScale.minute;
    public Sprite icon;
    public bool isPremade = true;
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