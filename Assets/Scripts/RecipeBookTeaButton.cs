using UnityEngine;
using UnityEngine.UI;

public class RecipeBookTeaButton : MonoBehaviour
{
    [Header("UI")]
    public Image teaIcon;
    public Text teaName;
    public Text vibeText;
    public Text ownedText;
    public Button button;

    private Teabag tea;
    private int owned;
    private void Awake()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(Select);
    }
    public void Setup(Teabag newTea, int newOwned)
    {
        tea = newTea;
        owned = newOwned;
        gameObject.SetActive(true);
        if (teaName != null)
            teaName.text = tea.teaName;
        if (vibeText != null)
            vibeText.text = tea.description;
        if (ownedText != null)
            ownedText.text = "x" + owned;
        if (teaIcon != null)
        {
            teaIcon.sprite = TeaShopManager.I.GetVibeIcon(tea.teaVibe);
            teaIcon.enabled = teaIcon.sprite != null;
        }
        if (button != null)
            button.interactable = owned > 0;
    }
    public void Clear()
    {
        tea = null;
        owned = 0;

        if (teaIcon != null)
        {
            teaIcon.sprite = null;
            teaIcon.enabled = false;
        }

        if (teaName != null)
            teaName.text = "";

        if (vibeText != null)
            vibeText.text = "";

        if (ownedText != null)
            ownedText.text = "";

        if (button != null)
            button.interactable = false;
    }
    public void Select()
    {
        if (tea == null ||
            owned <= 0)
            return;

        UIManager.I.CallContextUI(this);
    }
    public Teabag GetTea()
    {
        return tea;
    }
    public int GetOwned()
    {
        return owned;
    }
    public void Brew()
    {
        if (tea == null ||
            owned <= 0)
            return;

        RecipeBookUIManager.I.SelectTea(
            tea
        );
    }
}