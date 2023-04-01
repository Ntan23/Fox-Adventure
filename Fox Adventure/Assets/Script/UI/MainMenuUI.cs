using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Settings settings;

    void Start()
    {
        playButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.LevelSelection);
        });

        settingButton.onClick.AddListener(() => {
            settings.ShowSettings();
        }); 

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
