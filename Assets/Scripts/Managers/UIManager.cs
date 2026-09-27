using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager I;
    public GameObject contextPanel;
    [Header("Buttons")]
    public Button contextButton1, contextButton2, contextButton3;
    [Header("Texts")]
    public Text contextTitle, contextDescription;

    MonoBehaviour currentCaller;
    private void Awake()
    {
        I = this;
        ResetContextUI();
    }
    private void OnDestroy() => I = null;
    public void CallContextUI(MonoBehaviour caller)
    {
        currentCaller = caller;
        ResetContextUI();
        contextPanel.SetActive(true);
        switch (caller)
        {
            case CustomerChair chair:
                if (!chair.isUnlocked)
                {
                    contextTitle.text = "Chair";
                    contextDescription.text = "Locked Chair";
                    SetupButton(contextButton1, "Unlock Chair", () =>
                    {
                        chair.UnlockChair();
                        RefreshContextUI();
                    });
                    return;
                }
                if (chair.isOccupied)
                {
                    contextTitle.text = "Chair";
                    contextDescription.text = "Occupied Chair";
                    SetupButton(contextButton1, "Fulfill Order", () =>
                    {
                        chair.CallCustomerUp();
                        RefreshContextUI();
                    });
                    SetupButton(contextButton2, "Dismiss", () =>
                    {
                        chair.DismissCustomer();
                        RefreshContextUI();
                    });
                }
                break;
            case PlantPlot plot:
                if (!plot.saveData.isUnlocked)
                {
                    contextTitle.text = "" + plot.heldData.plantName;
                    contextDescription.text = "Unlock Plant Plot for " + GardenManager.I.unlockCost();
                    SetupButton(contextButton1, "Unlock", () =>
                    {
                        plot.UnlockPlot();
                        RefreshContextUI();
                    });
                }
                if (plot.saveData.harvestCount > 0)
                {
                    contextTitle.text = "" + plot.heldData.plantName;
                    contextDescription.text = "Harvest Plant";
                    SetupButton(contextButton1, "Harvest", () => { plot.HarvestPlot(); RefreshContextUI(); });
                    SetupButton(contextButton2, "Harvest All", () =>
                    {
                        GardenManager.I.HarvestAll();
                        RefreshContextUI();
                    });
                }
                break;
            case CraftingIngredientButton ingredient:
                contextTitle.text = ingredient.plant.plantName;
                contextDescription.text = "Vibe: " + ingredient.plant.vibe.ToString();
                SetupButton(contextButton1, "Add to Mixer", () => { CraftingManager.I.AddToMixer(ingredient.plant); CloseContextUI(); });
                break;
            case CustomerUI customerUI:
                Customer customer = customerUI.customer;
                if (customer == null)
                    break;
                contextTitle.text = customer.customerName;
                contextDescription.text = customer.order != null ? customer.order.requestText : "";
                if (customer.chairID >= 0)
                {
                    SetupButton(
                        contextButton1,
                        "Call",
                        () =>
                        {
                            int chairID = customer.chairID;

                            SeatingManager.I
                                .CallCustomerFromSeat(chairID);

                            CloseContextUI();
                        }
                    );
                    SetupButton(
                        contextButton2,
                        "Dismiss",
                        () =>
                        {
                            CustomerQueueManager.I
                                .DismissCustomer(customer);

                            CloseContextUI();
                        }
                    );
                    break;
                }
                if (SeatingManager.I.HasFreeSeat())
                {
                    SetupButton(
                        contextButton1,
                        "Seat",
                        () =>
                        {
                            CustomerQueueManager.I
                                .SendToChair(customer);

                            CloseContextUI();
                        }
                    );
                }
                SetupButton(
                    contextButton2,
                    "Serve",
                    () =>
                    {
                        BrewedTeaUIManager.I.Open(customer);
                        CloseContextUI();
                    }
                );
                SetupButton(
                    contextButton3,
                    "Dismiss",
                    () =>
                    {
                        CustomerQueueManager.I
                            .DismissCustomer(customer);

                        CloseContextUI();
                    }
                );
                break;
            case BrewedTeaButton brewed:
                Teabag brew = brewed.GetTea();
                if (brew == null)
                    break;
                contextTitle.text = brew.teaName;
                contextDescription.text = GetTeaVibeDescription(brew);
                SetupButton(
                    contextButton1,
                    "Confirm Serve",
                    () =>
                    {
                        BrewedTeaUIManager.I
                            .ConfirmServe(brew);

                        CloseContextUI();
                    }
                );
                break;
            case RecipeBookTeaButton recipe:
                Teabag tea = recipe.GetTea();
                if (tea == null)
                    return;
                contextTitle.text = tea.teaVibe.ToString();
                contextDescription.text = BuildTeaRecipeDescription(tea);
                SetupButton(contextButton1, "Brew", () => { recipe.Brew(); CloseContextUI(); });
                break;
        }
    }
    void ResetContextUI()
    {
        contextPanel.SetActive(false);
        ResetButton(contextButton1);
        ResetButton(contextButton2);
        ResetButton(contextButton3);
        contextTitle.text = "";
        contextDescription.text = "";
    }
    void ResetButton(Button button)
    {
        button.onClick.RemoveAllListeners();
        button.gameObject.SetActive(false);
    }
    void SetupButton(Button button, string text, UnityEngine.Events.UnityAction action)
    {
        button.GetComponentInChildren<Text>().text = text;
        button.onClick.AddListener(action);
        button.gameObject.SetActive(true);
    }
    void RefreshContextUI()
    {
        if (currentCaller == null)
        {
            CloseContextUI();
            return;
        }
        CallContextUI(currentCaller);
    }
    public void CloseContextUI()
    {
        currentCaller = null;
        ResetContextUI();
    }
    string GetTeaVibeDescription(Teabag tea)
    {
        string result = "";
        if (tea == null || tea.vibes == null || tea.vibes.Count == 0)
            return result;
        foreach (VibeValue value in tea.vibes)
        {
            if (result != "")
                result += "\n";
            result += value.type.ToString() + ": " + value.value;
        }
        return result;
    }
    string BuildTeaRecipeDescription(Teabag tea)
    {
        if (tea == null || tea.ingredients == null || tea.ingredients.Count == 0)
            return "No ingredients.";
        string description = "";
        for (int i = 0; i < tea.ingredients.Count; i++)
        {
            PlantData plant = PlantDatabase.newPlant(tea.ingredients[i]);
            if (plant == null)
                continue;
            description += plant.plantName;
            if (i < tea.ingredients.Count - 1)
                description += ", ";
        }
        return description;
    }
    public void Quit()
    {
        GameManager.I.Save();
        Application.Quit();
    }
}