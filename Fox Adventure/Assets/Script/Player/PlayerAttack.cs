using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float jumpForceAfterAttack;
    private PlayerController playerController;
    private AudioManager audioManager;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        audioManager = AudioManager.Instance;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && playerController.IsFalling())
        {
            other.gameObject.GetComponent<Enemy>().SetDeathAnimation();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForceAfterAttack);
            audioManager.PlayHitSFX();
        } 
    }
}
