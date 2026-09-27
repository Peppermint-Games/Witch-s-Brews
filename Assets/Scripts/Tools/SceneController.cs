using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController I; 
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }
    public string teashopScene, gardenScene, menuScene, tutorialScene;
    public void GoToGarden() => SceneManager.LoadScene(gardenScene);
    public void GoToTeaShop() => SceneManager.LoadScene(teashopScene);
    public void StartTutorial() => SceneManager.LoadScene(tutorialScene);
    public void StartGame() => SceneManager.LoadScene(gardenScene);
}