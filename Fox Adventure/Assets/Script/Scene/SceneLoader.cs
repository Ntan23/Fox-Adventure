using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene {
        MainMenu, LoadingScene, DifficultySelector, Level1
    }
    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Time.timeScale = 1;
        SceneLoader.targetScene = targetScene;

        SceneManager.LoadSceneAsync(Scene.LoadingScene.ToString());
    }

    public static void SceneLoaderCallback()
    {
        SceneManager.LoadSceneAsync(targetScene.ToString());
    }

    public static void ReloadScene()
    {   
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
