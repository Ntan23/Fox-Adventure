using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private float jumpForceAfterAttack;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && playerController.IsFalling())
        {
            other.gameObject.GetComponent<Enemy>().SetDeathAnimation();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForceAfterAttack);
        } 
    }
}
