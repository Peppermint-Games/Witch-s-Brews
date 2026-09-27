using UnityEngine;
using UnityEngine.UI;

public class BrewedTeaButton : MonoBehaviour
{
    public Image icon;

    private Teabag tea;
    private int count;

    public void Setup(
        Teabag newTea,
        int newCount)
    {
        tea = newTea;
        count = newCount;

        if (icon != null)
        {
            icon.sprite =
                TeaShopManager.I
                    .GetVibeIcon(
                        tea.teaVibe
                    );

            icon.enabled =
                icon.sprite != null;
        }
        this.GetComponent<Button>().onClick.AddListener(() => Interact());
    }

    public void Interact()
    {
        if (tea == null)
            return;

        UIManager.I.CallContextUI(this);
    }

    public Teabag GetTea()
    {
        return tea;
    }

    public int GetCount()
    {
        return count;
    }
}