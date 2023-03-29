using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablesCountUI : MonoBehaviour
{
    private bool firstTime = true;
    private GameManager gm;
    [SerializeField] private TextMeshProUGUI collectablesCountText;

    void Start()
    {
        gm = GameManager.Instance;

        UpdateCollectablesCountUI();
    }

    public void UpdateCollectablesCountUI()
    {
        collectablesCountText.text = gm.GetCollectedItemCount().ToString() + " / " + gm.GetMaxCollectedItemCount().ToString();
        if(!firstTime) StartCoroutine(Animation());
        firstTime = false;
    }

    IEnumerator Animation()
    {
        LeanTween.scale(gameObject, new Vector3(1.2f,1.2f,1.2f), 0.3f);
        yield return new WaitForSeconds(0.2f);
        LeanTween.scale(gameObject, new Vector3(1.0f,1.0f,1.0f), 0.3f);
    }
}
