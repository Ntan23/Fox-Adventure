using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Settings settings;
    GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;

        resumeButton.onClick.AddListener(() => {
            StartCoroutine(UnhideMenu());
        });

        settingButton.onClick.AddListener(() => {
            settings.ShowSettings();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });

        gameObject.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        gameObject.SetActive(true);
        gm.PauseGame();

        void UpdateAlpha(float alpha)
        {
            GetComponent<CanvasGroup>().alpha = alpha;

            if(GetComponent<CanvasGroup>().alpha == 1)
            {
                resumeButton.interactable = true;
                settingButton.interactable = true;
                mainMenuButton.interactable = true;
            }
        }

        LeanTween.value(gameObject, UpdateAlpha, 0f, 1f, 0.5f);
    }

    IEnumerator UnhideMenu()
    {
        void UpdateAlpha(float alpha)
        {
            GetComponent<CanvasGroup>().alpha = alpha;
        }

        LeanTween.value(gameObject, UpdateAlpha, 1f, 0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        gm.UnpauseGame();
    }
}
