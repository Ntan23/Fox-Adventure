using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.LevelSelection);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
