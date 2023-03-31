using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIconAnimation : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ScaleUpAndDownAnimation());
    }

    IEnumerator ScaleUpAndDownAnimation()
    {
        while(enabled)
        {
            LeanTween.scale(gameObject, new Vector3(1.0f, 1.0f, 1.0f), 1.0f);
            yield return new WaitForSeconds(1.0f);
            LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), 1.0f);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
