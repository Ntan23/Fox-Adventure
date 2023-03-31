using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private SceneLoader.Scene nextScene;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;

        retryButton.onClick.AddListener(() => {
            SceneLoader.ReloadScene();
        });

        nextLevelButton.onClick.AddListener(() => {
            SceneLoader.Load(nextScene);
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });

        gameObject.SetActive(false);
    }

    public void ShowVictoryUI()
    {
        gameObject.SetActive(true);

        if(gm.MaxLevel()) nextLevelButton.gameObject.SetActive(false);

        void UpdateAlpha(float alpha)
        {
            GetComponent<CanvasGroup>().alpha = alpha;

            if(GetComponent<CanvasGroup>().alpha == 1)
            {
                retryButton.interactable = true;

                nextLevelButton.interactable = true;
                
                nextLevelButton.interactable = true;
                
                mainMenuButton.interactable = true;
            }
        }

        LeanTween.value(gameObject, UpdateAlpha, 0f, 1f, 1f);
    }
}
