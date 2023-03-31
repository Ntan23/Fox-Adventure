using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimationControl : Enemy
{
    [SerializeField] private LayerMask groundLayer;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if(animator.GetBool("Jumping"))
        {
            if(rb.velocity.y < 0.1f) 
            {
                animator.SetBool("Jumping",false);
                animator.SetBool("Falling",true);
            }
        }

        if(enemyCollider.IsTouchingLayers(groundLayer) && animator.GetBool("Falling"))
        {
            animator.SetBool("Jumping",false);
            animator.SetBool("Falling",false);
        }
    }

    public void SetJumpAnimation()
    {
        animator.SetBool("Jumping",true);
    }
}
