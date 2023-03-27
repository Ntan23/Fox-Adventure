using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    #endregion

    #region Variables
    private int collectedItemCount;
    [SerializeField] private CollectablesCountUI collectablesCountUI;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCollectedItemCount()
    {
        collectedItemCount++;
        collectablesCountUI.UpdateCollectablesCountUI();
    }

    public int GetCollectedItemCount()
    {   
        return collectedItemCount;
    }
}
