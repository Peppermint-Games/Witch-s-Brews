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
    public Image contextIcon;

    MonoBehaviour currentCaller;
    private void Awake()
    {
        I = this;
    }
    private void OnDestroy() => I = null;
    public void CallContextUI(MonoBehaviour caller)
    {
        currentCaller = caller;
        ResetContextUI();
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
                break;
        }
    }
    void ResetContextUI()
    {
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
        if(currentCaller == null)
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
}