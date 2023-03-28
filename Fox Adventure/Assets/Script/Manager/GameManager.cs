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
    [SerializeField] private GameObject player;
    private Vector3 playerInitialPosition;
    private int collectedItemCount;
    [SerializeField] private int maxCollectedItemCount;
    [SerializeField] private CollectablesCountUI collectablesCountUI;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerInitialPosition = player.transform.position;
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

    public int GetMaxCollectedItemCount()
    {
        return maxCollectedItemCount;
    }

    public void Respawn()
    {
        player.transform.position = playerInitialPosition;
    }
}
