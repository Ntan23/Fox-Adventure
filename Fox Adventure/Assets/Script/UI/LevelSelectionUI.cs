using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private Button[] levelButton;
    [SerializeField] private Button backButton;
    private int levelUnlocked;
    
    void Start()
    {
        levelUnlocked = PlayerPrefs.GetInt("LevelUnlocked",1);

        CheckLevel();

        levelButton[0].onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.Level1);
        });

        levelButton[1].onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.Level2);
        });

        levelButton[2].onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.Level3);
        });

        backButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });
    }

    void CheckLevel()
    {
        for(int i = 0; i < levelButton.Length; i++)
        {
            if(i < levelUnlocked) levelButton[i].interactable = true;
            else levelButton[i].interactable = false;
        }
    }
}
