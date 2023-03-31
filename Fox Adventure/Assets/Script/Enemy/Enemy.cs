using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D enemyCollider;
    protected bool canMove = true;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
    }

    public void SetDeathAnimation()
    {
        animator.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        enemyCollider.enabled = false;
        canMove = false;
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
