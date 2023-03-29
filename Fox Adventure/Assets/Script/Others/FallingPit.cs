using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPit : MonoBehaviour
{
    private GameManager gm;
    private AudioManager audioManager;

    void Start()
    {
        gm = GameManager.Instance;
        audioManager = AudioManager.Instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) 
        {
            gm.Respawn();
            audioManager.PlayFallToPitSFX();
        }
    }
}
