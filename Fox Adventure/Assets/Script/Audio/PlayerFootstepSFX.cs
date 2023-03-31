using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSFX : MonoBehaviour
{
    #region FloatVariables
    private float footstepTimer;
    private float maxFootstepTimer = 0.3f;
    #endregion

    #region OtherVariables
    private AudioManager audioManager;
    private PlayerController playerController;
    private GameManager gm;
    #endregion

    void Start()
    {
        audioManager = AudioManager.Instance;
        gm = GameManager.Instance;

        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        footstepTimer += Time.deltaTime;

        if(footstepTimer > maxFootstepTimer)
        {
            footstepTimer = 0;

            if(playerController.IsRunning() && gm.IsPlaying()) audioManager.PlayFootstepSFX();
        }
    }
}
