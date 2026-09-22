using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController I;
    private void Awake()
    {
        I = this;
    }
    public string teashopScene, gardenScene;
    public void GoToGarden() => StartCoroutine(SwitchArea(teashopScene, gardenScene));
    public void GoToTeaShop() => StartCoroutine(SwitchArea(gardenScene, teashopScene));
    IEnumerator SwitchArea(string unloadScene, string loadScene)
    {
        Scene oldScene = SceneManager.GetSceneByName(unloadScene);
        if (oldScene.isLoaded)
            yield return SceneManager.UnloadSceneAsync(unloadScene);
        Scene newScene = SceneManager.GetSceneByName(loadScene);
        if (!newScene.isLoaded)
            yield return SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadScene));
    }
}