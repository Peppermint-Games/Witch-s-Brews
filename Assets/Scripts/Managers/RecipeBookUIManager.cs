using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookUIManager : MonoBehaviour
{
    public static RecipeBookUIManager I;

    [Header("Main Panel")]
    public GameObject recipeBookPanel;
    [Header("Left Page")]
    public Text leftPageNumber;
    public RecipeBookTeaButton[] leftButtons =        new RecipeBookTeaButton[4];
    [Header("Right Page")]
    public Text rightPageNumber;
    public RecipeBookTeaButton[] rightButtons =        new RecipeBookTeaButton[4];
    [Header("Navigation")]
    public Button previousButton;
    public Button nextButton;
    public Button closeButton;
    private Kettle selectedKettle;
    private int currentSpread = 0;
    private const int RecipesPerPage = 4;
    private const int RecipesPerSpread = 8;
    private List<Teabag> recipes =        new List<Teabag>();

    // =========================================================
    // UNITY
    // =========================================================

    private void Awake()
    {
        I = this;
        if (recipeBookPanel != null)
            recipeBookPanel.SetActive(false);
        if (previousButton != null)
        {
            previousButton.onClick.RemoveListener(
                PreviousSpread
            );

            previousButton.onClick.AddListener(
                PreviousSpread
            );
        }

        if (nextButton != null)
        {
            nextButton.onClick.RemoveListener(
                NextSpread
            );

            nextButton.onClick.AddListener(
                NextSpread
            );
        }

        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(
                Close
            );

            closeButton.onClick.AddListener(
                Close
            );
        }
    }

    private void OnDestroy()
    {
        if (I == this)
            I = null;
    }

    // =========================================================
    // OPEN / CLOSE
    // =========================================================

    public void OpenForKettle(Kettle kettle)
    {
        if (kettle == null)
            return;

        if (!kettle.IsEmpty())
            return;

        selectedKettle = kettle;
        currentSpread = 0;

        BuildRecipeList();

        if (recipeBookPanel != null)
            recipeBookPanel.SetActive(true);

        Refresh();
    }

    public void Close()
    {
        selectedKettle = null;

        if (recipeBookPanel != null)
            recipeBookPanel.SetActive(false);
    }

    // =========================================================
    // RECIPE LIST
    // =========================================================

    private void BuildRecipeList()
    {
        recipes.Clear();

        if (CraftingManager.I == null)
        {
            Debug.LogWarning(
                "Recipe Book: CraftingManager not found."
            );

            return;
        }

        CraftingManager.I.BuildRecipeBook();

        int count =
            CraftingManager.I.RecipeCount();

        for (int i = 0; i < count; i++)
        {
            Teabag tea =
                CraftingManager.I.GetRecipeAt(i);

            if (tea != null)
                recipes.Add(tea);
        }

        Debug.Log(
            "Recipe Book loaded " +
            recipes.Count +
            " recipes."
        );
    }

    // =========================================================
    // NAVIGATION
    // =========================================================

    public void NextSpread()
    {
        int spreadCount =
            GetSpreadCount();

        if (spreadCount <= 1)
            return;

        currentSpread++;

        if (currentSpread >= spreadCount)
            currentSpread = 0;

        Refresh();
    }

    public void PreviousSpread()
    {
        int spreadCount =
            GetSpreadCount();

        if (spreadCount <= 1)
            return;

        currentSpread--;

        if (currentSpread < 0)
            currentSpread =
                spreadCount - 1;

        Refresh();
    }

    private int GetSpreadCount()
    {
        if (recipes.Count <= 0)
            return 1;

        return Mathf.CeilToInt(
            recipes.Count /
            (float)RecipesPerSpread
        );
    }

    // =========================================================
    // DISPLAY
    // =========================================================

    public void Refresh()
    {
        for (int i = 0; i < rightButtons.Length; i++)
        {
            if (rightButtons[i] == null)
            {
                Debug.LogError("RIGHT BUTTON " + i + " IS NULL");
                continue;
            }

            rightButtons[i].gameObject.SetActive(true);

            Text[] texts =
                rightButtons[i].GetComponentsInChildren<Text>(true);

            Debug.Log(
                "RIGHT " + i +
                " | Object=" + rightButtons[i].gameObject.name +
                " | activeSelf=" + rightButtons[i].gameObject.activeSelf +
                " | activeHierarchy=" + rightButtons[i].gameObject.activeInHierarchy +
                " | Text children=" + texts.Length
            );

            foreach (Text text in texts)
            {
                text.text = "RIGHT TEST";
                text.enabled = true;
            }
        }
        int startIndex =
            currentSpread *
            RecipesPerSpread;

        // -----------------------------------------
        // Page numbers
        // -----------------------------------------

        int leftPage =
            currentSpread * 2 + 1;

        int rightPage =
            leftPage + 1;

        if (leftPageNumber != null)
        {
            leftPageNumber.text =
                leftPage.ToString();
        }

        if (rightPageNumber != null)
        {
            rightPageNumber.text =
                rightPage.ToString();
        }

        // -----------------------------------------
        // Left page
        // -----------------------------------------

        for (int i = 0;
             i < leftButtons.Length;
             i++)
        {
            int recipeIndex =
                startIndex + i;

            DisplayRecipe(
                leftButtons[i],
                recipeIndex
            );
        }

        // -----------------------------------------
        // Right page
        // -----------------------------------------

        for (int i = 0;
             i < rightButtons.Length;
             i++)
        {
            int recipeIndex =
                startIndex +
                RecipesPerPage +
                i;

            DisplayRecipe(
                rightButtons[i],
                recipeIndex
            );
        }

        // -----------------------------------------
        // Navigation
        // -----------------------------------------

        int spreadCount =
            GetSpreadCount();

        bool canChangePage =
            spreadCount > 1;

        if (previousButton != null)
        {
            previousButton.interactable =
                canChangePage;
        }

        if (nextButton != null)
        {
            nextButton.interactable =
                canChangePage;
        }

        Debug.Log(
            "Recipe Book | Spread " +
            currentSpread +
            " | Recipes " +
            startIndex +
            " - " +
            (startIndex + 7) +
            " | Total: " +
            recipes.Count
        );
    }
    private void DisplayRecipe(
    RecipeBookTeaButton recipeButton,
    int recipeIndex)
    {
        if (recipeButton == null)
        {
            Debug.LogError(
                "BUTTON NULL | Recipe index: " +
                recipeIndex
            );
            return;
        }

        Debug.Log(
            "BUTTON: " +
            recipeButton.gameObject.name +
            " | INDEX: " +
            recipeIndex +
            " | ACTIVE: " +
            recipeButton.gameObject.activeInHierarchy +
            " | TOTAL RECIPES: " +
            recipes.Count
        );

        recipeButton.gameObject.SetActive(true);

        if (recipeIndex < 0 ||
            recipeIndex >= recipes.Count)
        {
            Debug.LogWarning(
                "NO RECIPE FOR " +
                recipeButton.gameObject.name +
                " | index " +
                recipeIndex
            );

            recipeButton.Clear();
            return;
        }

        Teabag tea = recipes[recipeIndex];

        if (tea == null)
        {
            Debug.LogError(
                "TEA NULL AT INDEX " +
                recipeIndex
            );

            recipeButton.Clear();
            return;
        }

        int owned = 0;

        if (TeaShopManager.I != null)
        {
            owned =
                TeaShopManager.I.GetTeabagCount(
                    tea.id
                );
        }

        Debug.Log(
            "ASSIGNING " +
            tea.teaName +
            " -> " +
            recipeButton.gameObject.name
        );

        recipeButton.Setup(
            tea,
            owned
        );
    }

    // =========================================================
    // BREW
    // =========================================================

    public void SelectTea(Teabag tea)
    {
        if (tea == null)
            return;

        if (selectedKettle == null)
        {
            Debug.LogWarning(
                "Cannot brew: no kettle selected."
            );

            return;
        }

        if (!selectedKettle.IsEmpty())
        {
            Debug.LogWarning(
                "Cannot brew: selected kettle " +
                "is no longer empty."
            );

            Close();
            return;
        }

        if (TeaShopManager.I == null)
        {
            Debug.LogWarning(
                "Cannot brew: TeaShopManager " +
                "not found."
            );

            return;
        }

        bool success =
            TeaShopManager.I.BrewTea(
                selectedKettle,
                tea.id
            );

        if (!success)
        {
            Debug.LogWarning(
                "BrewTea failed for " +
                tea.teaName
            );

            Refresh();
            return;
        }

        Debug.Log(
            "Started brewing " +
            tea.teaName
        );

        Close();
    }
}