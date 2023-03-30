using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    void Start()
    {
        retryButton.onClick.AddListener(() => {
            SceneLoader.ReloadScene();
        });

        gameObject.SetActive(false);
    }

    public void ShowLoseUI()
    {
        gameObject.SetActive(true);
        
        void UpdateAlpha(float alpha)
        {
            GetComponent<CanvasGroup>().alpha = alpha;

            if(GetComponent<CanvasGroup>().alpha == 1)
            {
                retryButton.interactable = true;
                mainMenuButton.interactable = true;
            }
        }

        LeanTween.value(gameObject, UpdateAlpha, 0f, 1f, 1f);
    }
}
