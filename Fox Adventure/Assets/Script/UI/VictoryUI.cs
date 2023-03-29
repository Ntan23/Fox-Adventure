using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(() => {
            SceneLoader.ReloadScene();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowVictoryUI()
    {
        void UpdateAlpha(float alpha)
        {
            GetComponent<CanvasGroup>().alpha = alpha;

            if(GetComponent<CanvasGroup>().alpha == 1)
            {
                retryButton.interactable = true;
                nextLevelButton.interactable = true;
                mainMenuButton.interactable = true;
            }
        }

        LeanTween.value(gameObject, UpdateAlpha, 0f, 1f, 1f);
    }
}
