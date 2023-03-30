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
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    [Header("Hurt IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    public bool IsInvunerable = false;

    [Header("Collectables")]
    [SerializeField] private GameObject[] cherries;
    [SerializeField] private PowerUp[] powerUps;

    private int collectedItemCount;
    private int maxCollectedItemCount;
    [SerializeField] private CollectablesCountUI collectablesCountUI;

    [Header("Enemy")]
    [SerializeField] private Eagle[] eagles;

    [Header("Others")]
    [SerializeField] private MustCollectUI mustCollectUI;
    [SerializeField] private VictoryUI victoryUI;
    [SerializeField] private LoseUI loseUI;
    private int maxLevelCount = 3;
    [SerializeField] private int currentLevel;
    private AudioManager audioManager;
    #endregion

    void Start()
    {
        playerRespawnPosition = player.transform.position;
        audioManager = AudioManager.Instance;

        playerLivesCount = 3;
        gameState = State.Playing;
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
        if(!IsInvunerable) LoseLive();
        if(eagles != null) RespawnEagles();
        if(powerUps != null) RespawnPowerUps();
    }

    public void CheckSituation()
    {
        if(collectedItemCount < maxCollectedItemCount) 
        {
            mustCollectUI.ShowMustCollectUI();
            audioManager.PlayUIPopUpSFX();
        }
        else if(collectedItemCount == maxCollectedItemCount) 
        {
            victoryUI.ShowVictoryUI();
            audioManager.PlayVictorySFX();
            gameState = State.GameOver;
        }
    }

    public void RespawnEagles()
    {
        foreach(Eagle eagle in eagles)
        {
            if(!eagle.gameObject.activeInHierarchy) eagle.gameObject.SetActive(true);
        }
    }

    public void RespawnPowerUps()
    {
        foreach(PowerUp powerUp in powerUps)
        {
            if(!powerUp.gameObject.activeInHierarchy) powerUp.gameObject.SetActive(true);
        }
    }

    public void LoseLive()
    {
        playerLivesCount--;
        playerHealth[playerLivesCount].SetActive(false);

        StartCoroutine(Invunerability());

        if(playerLivesCount == 0) 
        {
            gameState = State.GameOver;
            loseUI.ShowLoseUI();
            audioManager.PlayLoseSFX();
        }
    }

    public bool IsPlaying()
    {
        return gameState == State.Playing;
    }

    public bool EagleIsNull()
    {
        return eagles == null;
    }

    public bool PowerUpIsNull()
    {
        return powerUps == null;
    }

    public bool MaxLevel()
    {
        return currentLevel == maxLevelCount;
    }

    IEnumerator Invunerability()
    {
        IsInvunerable = true;
        Physics2D.IgnoreLayerCollision(7, 0,true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            playerSpriteRenderer.color = new Color(1,0,0,0.8f);
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
            playerSpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
        }
        Physics2D.IgnoreLayerCollision(7, 0,false);
        IsInvunerable=false;
    }
}
