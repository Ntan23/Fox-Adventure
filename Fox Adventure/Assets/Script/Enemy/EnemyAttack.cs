using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float knockbackForce;

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
        if(other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PlayerController>().IsFalling())
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            
            other.gameObject.GetComponent<PlayerController>().GetHurt();

            if(transform.position.x > other.gameObject.transform.position.x) rb.velocity = new Vector2(-knockbackForce, rb.velocity.y);
            else if(transform.position.x < other.gameObject.transform.position.x ) rb.velocity = new Vector2(knockbackForce, rb.velocity.y);
        }
    }
}
