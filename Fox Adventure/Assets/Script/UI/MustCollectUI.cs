using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustCollectUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMustCollectUI()
    {
        StartCoroutine(FadeInFadeOut());
    }

    IEnumerator FadeInFadeOut()
    {
        void UpdateAlpha(float alpha)
        {
            GetComponent<CanvasGroup>().alpha = alpha;
        }

        LeanTween.value(gameObject, UpdateAlpha, 0f, 1f, 2f);
        yield return new WaitForSeconds(4.0f);
        LeanTween.value(gameObject, UpdateAlpha, 1f, 0f, 2f);
    }
}
