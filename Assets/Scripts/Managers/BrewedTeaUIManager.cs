using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewedTeaUIManager :
    MonoBehaviour
{
    public static BrewedTeaUIManager I;

    [Header("UI")]
    public GameObject panel;
    public Transform teaGrid;
    public Dropdown vibeDropdown;

    [Header("Prefab")]
    public GameObject teaButtonPrefab;

    private Customer selectedCustomer;

    private List<GameObject> generatedButtons =
        new List<GameObject>();

    private void Awake()
    {
        I = this;

        if (panel != null)
            panel.SetActive(false);
        SetupVibeDropdown();
    }

    public void Open(Customer customer)
    {
        if (customer == null)
            return;

        selectedCustomer = customer;

        panel.SetActive(true);

        BuildTeaGrid();
    }
    void SetupVibeDropdown()
    {
        if (vibeDropdown == null)
            return;

        vibeDropdown.ClearOptions();

        List<string> options =
            new List<string>();

        // Index 0 is always the unfiltered option.
        options.Add("All");

        // Automatically add every value from the vibe enum.
        string[] vibeNames =
            Enum.GetNames(typeof(vibe));

        foreach (string vibeName in vibeNames)
        {
            options.Add(vibeName);
        }

        vibeDropdown.AddOptions(options);

        vibeDropdown.value = 0;
        vibeDropdown.RefreshShownValue();

        vibeDropdown.onValueChanged.RemoveListener(
            OnVibeDropdownChanged
        );

        vibeDropdown.onValueChanged.AddListener(
            OnVibeDropdownChanged
        );
    }

    void OnVibeDropdownChanged(int value)
    {
        BuildTeaGrid();
    }
    public void Close()
    {
        selectedCustomer = null;

        ClearGrid();

        panel.SetActive(false);
    }

    // =========================================================
    // FILTER
    // =========================================================

    bool PassesFilter(Teabag tea)
    {
        if (tea == null)
            return false;

        if (vibeDropdown == null)
            return true;

        // 0 = All
        if (vibeDropdown.value == 0)
            return true;

        // Dropdown:
        // 1 = Energetic
        // 2 = Chill
        // ...
        vibe selected =
            (vibe)(
                vibeDropdown.value - 1
            );

        return tea.teaVibe == selected;
    }

    // =========================================================
    // GRID
    // =========================================================

    public void BuildTeaGrid()
    {
        ClearGrid();

        if (TeaShopManager.I == null ||
            TeaShopManager.I.thisData == null)
            return;

        foreach (TeaInv item in
                 TeaShopManager.I
                     .thisData
                     .brewedInventory)
        {
            if (item == null ||
                item.count <= 0)
                continue;

            Teabag tea =
                TeaShopManager.I
                    .GetTeaByID(
                        item.teaID
                    );

            if (tea == null)
                continue;

            if (!PassesFilter(tea))
                continue;

            GameObject obj =
                Instantiate(
                    teaButtonPrefab,
                    teaGrid
                );

            BrewedTeaButton button =
                obj.GetComponent<
                    BrewedTeaButton>();

            if (button != null)
            {
                button.Setup(
                    tea,
                    item.count
                );
            }

            generatedButtons.Add(obj);
        }
    }

    void ClearGrid()
    {
        foreach (GameObject obj
                 in generatedButtons)
        {
            if (obj != null)
                Destroy(obj);
        }

        generatedButtons.Clear();
    }

    // =========================================================
    // SERVE
    // =========================================================

    public void ConfirmServe(Teabag tea)
    {
        if (selectedCustomer == null ||
            tea == null)
            return;

        if (TeaShopManager.I
            .GetBrewedTeaCount(
                tea.id
            ) <= 0)
            return;

        bool removed =
            TeaShopManager.I
                .RemoveBrewedTea(
                    tea.id,
                    1
                );

        if (!removed)
            return;

        Customer customer =
            selectedCustomer;

        Close();

        TeaShopManager.I.ServeTea(
            customer,
            tea
        );

        CustomerQueueManager.I
            .CustomerServed(
                customer
            );
    }
}