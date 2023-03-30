using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void SetAnimation(int state)
    {
        animator.SetInteger("State",state);
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.speed = speed;
    }
}
