using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDeathAnimation()
    {
        animator.SetTrigger("Death");
        rb.velocity = Vector2.zero;
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
