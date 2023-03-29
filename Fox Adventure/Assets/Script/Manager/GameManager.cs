using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton & References
    public static GameManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null) Instance = this;

        maxCollectedItemCount = cherries.Length;
    }
    #endregion

    #region EnumVariables
    private enum State {
        Playing , GameOver
    }

    private State gameState;
    #endregion

    #region Variables
    [Header("Player")]
    [SerializeField] private GameObject player;
    private Vector3 playerRespawnPosition;
    private int playerLivesCount;
    [SerializeField] private GameObject[] playerHealth;

    [Header("Collectables")]
    [SerializeField] private GameObject[] cherries;
    private int collectedItemCount;
    private int maxCollectedItemCount;
    [SerializeField] private CollectablesCountUI collectablesCountUI;

    [Header("Enemy")]
    [SerializeField] private Eagle[] eagles;

    [Header("Others")]
    [SerializeField] private MustCollectUI mustCollectUI;
    [SerializeField] private VictoryUI victoryUI;
    private AudioManager audioManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerRespawnPosition = player.transform.position;
        audioManager = AudioManager.Instance;

        playerLivesCount = 3;
        gameState = State.Playing;
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
        player.transform.position = playerRespawnPosition;
        LoseLive();
        ResetEaglesInScene();
    }

    public void CheckSituation()
    {
        if(collectedItemCount < maxCollectedItemCount) 
        {
            mustCollectUI.ShowMustCollectUI();
            audioManager.PlayUIPopUpSFX();
            ResetEaglesInScene();
        }
        else if(collectedItemCount == maxCollectedItemCount) 
        {
            victoryUI.ShowVictoryUI();
            audioManager.PlayVictorySFX();
            gameState = State.GameOver;
        }
    }

    public void ResetEaglesInScene()
    {
        foreach(Eagle eagle in eagles)
        {
            if(!eagle.gameObject.activeInHierarchy) eagle.gameObject.SetActive(true);
        }
    }

    public void LoseLive()
    {
        playerLivesCount--;
        playerHealth[playerLivesCount].SetActive(false);

        if(playerLivesCount == 0) 
        {
            gameState = State.GameOver;
            Time.timeScale = 0f;
            Debug.Log("You Lose");
        }
    }

    public bool IsPlaying()
    {
        return gameState == State.Playing;
    }
}
