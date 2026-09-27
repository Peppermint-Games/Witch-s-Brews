using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject creditUI;
    public string[] tutUI;
    public Text tutText;
    public int curTut;
    public bool isMainMenu;
    private void Update()
    {
        if (isMainMenu)
            return;
        curTut = Mathf.Clamp(curTut, 0, tutUI.Length);
        tutText.text = tutUI[curTut];
    }
    public void ContinueGame()
    {
        if (!File.Exists(SaveManager.SavePath))
            NewGame();
        else
        {
            GameManager.I.LoadGame();
            SceneController.I.StartGame();
        }
    }
    public void NewGame()
    {
        GameManager.I.Save();
        SceneController.I.StartTutorial();
    }
    public void ToggleCredits() => creditUI.SetActive(!creditUI.activeInHierarchy);
    public void QuitGame() => Application.Quit();
    public void AdvanceTutorial()
    {
        curTut++;
        if (curTut >= tutUI.Length)
            ContinueGame();
    }
    public void ReverseTutorial() => curTut--;
}