using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablesCountUI : MonoBehaviour
{
    GameManager gm;
    [SerializeField] private TextMeshProUGUI collectablesCountText;

    void Start()
    {
        gm = GameManager.Instance;
    }

    public void UpdateCollectablesCountUI()
    {
        collectablesCountText.text = gm.GetCollectedItemCount().ToString();
    }
}
